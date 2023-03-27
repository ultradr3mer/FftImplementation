using System;
using System.Linq;
using System.Numerics;

namespace FftImplementation
{
  internal class Fft
  {
    private int size;
    private readonly int[] arrangement;

    public Fft(int size)
    {
      this.size = size;

      this.arrangement = this.GetArrangement(Enumerable.Range(0, size).ToArray());
    }

    internal Complex[] Transform(Complex[] coefficients)
    {
      if (coefficients.Length != size)
      {
        throw new ArgumentException("Data of wrong size.", nameof(coefficients));
      }

      var unaranged = this.TransformStep(coefficients);

      return arrangement.Select(i => unaranged[i]).ToArray();
    }

    private int[] GetArrangement(int[] indices)
    {
      if (indices.Length == 1)
        return indices;

      int segmentSize = indices.Length / 2;

      var top = GetArrangement(Enumerable.Range(0, segmentSize).Select(i => indices[i * 2]).ToArray());
      var bottom = GetArrangement(Enumerable.Range(0, segmentSize).Select(i => indices[i * 2 + 1]).ToArray());

      return top.Concat(bottom).ToArray();
    }

    private Complex[] TransformStep(Complex[] coefficients)
    {
      if (coefficients.Length == 1)
        return coefficients;

      var w = Complex.Pow(Math.E, new Complex(0, 2 * Math.PI / coefficients.Length));
      int segmentSize = coefficients.Length / 2;

      var top = TransformStep(Enumerable.Range(0, segmentSize)
        .Select(i => coefficients[i] + coefficients[i + segmentSize]).ToArray());
      var bottom = TransformStep(Enumerable.Range(0, segmentSize)
        .Select(i => Complex.Pow(w, i) * (coefficients[i] - coefficients[i + segmentSize])).ToArray());

      return top.Concat(bottom).ToArray();
    }

    internal Complex[] Imvert(Complex[] wave)
    {
      return this.Transform(wave).Select(value => value / size).ToArray();
    }
  }
}