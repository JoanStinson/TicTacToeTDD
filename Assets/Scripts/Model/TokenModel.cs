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
                    return "<size=70><b>O</b></size>";

                case 1:
                    return "<size=70><b>X</b></size>";

                default:
                    return string.Empty;
            }
        }
    }
}