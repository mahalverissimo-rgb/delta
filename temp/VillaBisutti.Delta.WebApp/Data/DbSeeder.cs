using VillaBisutti.Delta.WebApp.Models;

namespace VillaBisutti.Delta.WebApp.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Locais.Any())
            {
                var locais = new List<Local>
                {
                    new Local
                    {
                        Nome = "Villa Bisutti Berrini",
                        Endereco = "Rua Berrini, 550 - São Paulo, SP",
                        Capacidade = 500,
                        Ativo = true,
                        Observacoes = "Casa principal"
                    },
                    new Local
                    {
                        Nome = "Villa Bisutti Morumbi",
                        Endereco = "Av. Morumbi, 8000 - São Paulo, SP",
                        Capacidade = 400,
                        Ativo = true,
                        Observacoes = "Casa secundária"
                    }
                };
                context.Locais.AddRange(locais);
            }

            if (!context.Cardapios.Any())
            {
                var cardapios = new List<Cardapio>
                {
                    new Cardapio
                    {
                        Nome = "Cardápio Premium",
                        Descricao = "Cardápio completo com opções premium",
                        PrecoPorPessoa = 250.00m,
                        Ativo = true
                    },
                    new Cardapio
                    {
                        Nome = "Cardápio Clássico",
                        Descricao = "Cardápio tradicional com opções clássicas",
                        PrecoPorPessoa = 180.00m,
                        Ativo = true
                    }
                };
                context.Cardapios.AddRange(cardapios);
            }

            if (!context.TiposServico.Any())
            {
                var tiposServico = new List<TipoServico>
                {
                    new TipoServico
                    {
                        Nome = "Casamento",
                        Descricao = "Cerimônia e recepção de casamento",
                        Ativo = true
                    },
                    new TipoServico
                    {
                        Nome = "Corporativo",
                        Descricao = "Eventos corporativos e empresariais",
                        Ativo = true
                    },
                    new TipoServico
                    {
                        Nome = "Aniversário",
                        Descricao = "Festas de aniversário",
                        Ativo = true
                    }
                };
                context.TiposServico.AddRange(tiposServico);
            }

            context.SaveChanges();
        }
    }
}
