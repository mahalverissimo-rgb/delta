namespace SGE.Models.Enums
{
    public static class HorarioExtensions
    {
        public static int ToInt(this Horario horario)
        {
            return (int)horario;
        }
    }

    public enum Horario
    {
        Manha,
        Tarde,
        Noite,
        Integral
    }
}
