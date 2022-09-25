namespace JGM.Model
{
    public class TokenModel
    {
        private readonly int value;

        public TokenModel(int value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            switch (value)
            {
                case 0:
                    return "O";

                case 1:
                    return "X";

                default:
                    return string.Empty;
            }
        }
    }
}