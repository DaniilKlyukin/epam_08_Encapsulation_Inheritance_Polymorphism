namespace TasksLibrary
{
    using System;

    public abstract class Shape
    {
        public abstract double Perimeter
        {
            get;
        }

        public abstract double Area
        {
            get;
        }
    }

    public class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException();

            this.radius = radius;
        }

        public override double Area
        {
            get
            {
                return Math.PI * radius * radius;
            }
        }

        public override double Perimeter
        {
            get
            {
                return 2 * Math.PI * radius;
            }

        }
    }

    public class Triangle : Shape
    {
        private double side1;
        private double side2;
        private double side3;

        public Triangle(double side1, double side2, double side3)
        {
            if ((side1 >= (side2 + side3) ||
                side2 >= (side1 + side3) ||
                side3 >= (side1 + side2)) ||
                (side1 <= 0 || side2 <= 0 || side3 <= 0))
            {
                throw new ArgumentException("Triangle not exist");
            }

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }

        public override double Area
        {
            get
            {
                var halfOfPerimeter = Perimeter / 2;

                return Math.Sqrt(
                    halfOfPerimeter *
                    (halfOfPerimeter - side1) *
                    (halfOfPerimeter - side2) *
                    (halfOfPerimeter - side3));
            }
        }

        public override double Perimeter
        {
            get
            {
                return side1 + side2 + side3;
            }
        }
    }

    public class Square : Shape
    {
        private double side;

        public Square(double side)
        {
            if (side <= 0)
                throw new ArgumentException();

            this.side = side;
        }

        public override double Area
        {
            get
            {
                return side * side;
            }
        }

        public override double Perimeter
        {
            get
            {
                return 4 * side;
            }
        }
    }

    public class Rectangle : Shape
    {
        private double side1;
        private double side2;

        public Rectangle(double side1, double side2)
        {
            if (side1 <= 0 || side2 <= 0)
                throw new ArgumentException();

            this.side1 = side1;
            this.side2 = side2;
        }

        public override double Area
        {
            get
            {
                return side1 * side2;
            }
        }

        public override double Perimeter
        {
            get
            {
                return 2 * side1 + 2 * side2;
            }
        }
    }
}
