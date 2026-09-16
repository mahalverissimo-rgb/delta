namespace VillaBisutti.Delta.WebApp.Models
{
    public class Horario
    {
        public int Horas { get; set; }
        public int Minutos { get; set; }

        public static Horario Parse(int value)
        {
            return new Horario
            {
                Horas = value / 100,
                Minutos = value % 100
            };
        }

        public int ToInt()
        {
            return (Horas * 100) + Minutos;
        }

        public override string ToString()
        {
            return $"{Horas:00}:{Minutos:00}";
        }
    }
}
