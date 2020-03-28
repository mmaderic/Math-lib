using System.ComponentModel;

namespace Math.Core.Enumerations
{
    public enum Operator
    {
        [Description("+")]
        Addition,

        [Description("-")]
        Subtraction,

        [Description("x")]
        Multiplication,

        [Description("/")]
        Division,

        [Description("")]
        None
    }
}
