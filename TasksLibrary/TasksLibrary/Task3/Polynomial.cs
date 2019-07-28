namespace TasksLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class Polynomial : IComparable<Polynomial>, IEnumerable<double>
    {
        public double[] Coefficients { get; private set; }

        public int Degree
        {
            get
            {
                var length = Coefficients.Length;

                for (int i = length - 1; i >= 0; i--)
                    if (Coefficients[i] != 0)
                        return i;

                return 0;
            }
        }

        double this[int index]
        {
            get
            {
                return this.Coefficients[index];
            }
            set
            {
                this.Coefficients[index] = value;
            }
        }

        private double[] FilterArray(double[] coefficients)
        {
            var length = coefficients.Length;
            for (int i = coefficients.Length - 1; i >= 0; i--)
            {
                if (coefficients[i] != 0)
                {
                    length = i + 1;
                    break;
                }
            }

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = coefficients[i];
            }

            return result;
        }

        public Polynomial(int degree)
        {
            Coefficients = new double[degree + 1];
        }

        public Polynomial(double[] coefficients)
        {
            coefficients = FilterArray(coefficients);

            var length = coefficients.Length;

            Coefficients = new double[length];

            for (int i = 0; i < length; i++)
            {
                Coefficients[i] = coefficients[i];
            }
        }

        public Polynomial(Dictionary<int, double> coefficients)
        {
            var length = coefficients.Keys.Max() + 1;

            Coefficients = new double[length];

            foreach (var c in coefficients)
            {
                Coefficients[c.Key] = c.Value;
            }
        }

        public int CompareTo(Polynomial other)
        {
            if (this.Degree != other.Degree)
                return this.Degree.CompareTo(other.Degree);

            for (int i = other.Degree; i >= 0; i--)
            {
                if (this[i] != other[i])
                    return this[i].CompareTo(other[i]);
            }

            return 0;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)Coefficients).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)Coefficients).GetEnumerator();
        }

        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            var degree = Math.Max(first.Degree, second.Degree);
            var length = degree + 1;

            var result = new Polynomial(degree);

            for (int i = 0; i < length; i++)
            {
                if (i <= first.Degree)
                    result[i] += first[i];

                if (i <= second.Degree)
                    result[i] += second[i];
            }

            return result.Simplify();
        }

        public static Polynomial operator -(Polynomial first, Polynomial second)
            => first + (-1) * second;

        public static Polynomial operator *(Polynomial poly, double factor)
            => poly
                .Select(x => x * factor)
                .ToPolynomial()
                .Simplify();

        public static Polynomial operator *(double factor, Polynomial poly)
            => poly * factor;

        public static Polynomial operator /(Polynomial poly, double divider)
            => poly
                .Select(x => x / divider)
                .ToPolynomial()
                .Simplify();

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            var degree = first.Degree + second.Degree;
            var length = degree + 1;

            var result = new Polynomial(degree);

            for (int i = 0; i <= first.Degree; i++)
            {
                for (int j = 0; j <= second.Degree; j++)
                {
                    result[i + j] += first[i] * second[j];
                }
            }

            return result.Simplify();
        }

        public static Tuple<Polynomial, Polynomial> operator /(Polynomial first, Polynomial second)
        {
            var remainder = (double[])first.Coefficients.Clone();
            var quotient = new double[remainder.Length - second.Coefficients.Length + 1];
            for (int i = 0; i < quotient.Length; i++)
            {
                double coeff = remainder[remainder.Length - i - 1] / first.Coefficients.Last();
                quotient[quotient.Length - i - 1] = coeff;
                for (int j = 0; j < second.Coefficients.Length; j++)
                {
                    remainder[remainder.Length - i - j - 1] -= coeff * second[second.Coefficients.Length - j - 1];
                }
            }

            return Tuple.Create(new Polynomial(quotient), new Polynomial(remainder));
        }

        public static bool operator >(Polynomial first, Polynomial second)
            => first.CompareTo(second) == 1 ? true : false;

        public static bool operator <(Polynomial first, Polynomial second)
            => first.CompareTo(second) == -1 ? true : false;

        public static bool operator ==(Polynomial first, Polynomial second)
            => first.CompareTo(second) == 0 ? true : false;

        public static bool operator !=(Polynomial first, Polynomial second)
            => first.CompareTo(second) != 0 ? true : false;

        public static bool operator >=(Polynomial first, Polynomial second)
            => first.CompareTo(second) != -1 ? true : false;

        public static bool operator <=(Polynomial first, Polynomial second)
            => first.CompareTo(second) != 1 ? true : false;

        private Polynomial Simplify()
        {
            var degree = this.Degree;
            var newPoly = new Polynomial(degree);

            for (int i = 0; i <= degree; i++)
                newPoly[i] = this[i];

            return newPoly;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= this.Degree; i++)
            {
                if (this[i] == 0 && i != 0) continue;
                if (i == 0)
                {
                    sb.Append(this[i] + " ");
                    continue;
                }
                if (this[i] > 0) sb.Append('+');
                if (i == 1)
                {
                    sb.Append(this[i] + "*x ");
                    continue;
                }

                sb.Append(this[i] + "*x^" + i + ' ');
            }

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            var hash = 0;

            for (int i = 0; i <= this.Degree; i++)
                hash += (int)Math.Pow(this[i], i);

            return hash;
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo((Polynomial)obj) == 0;
        }
    }

    public static class EnumerableExtensions
    {
        public static Polynomial ToPolynomial(this IEnumerable<double> collection)
        {
            return new Polynomial(collection.ToArray());
        }
    }
}
