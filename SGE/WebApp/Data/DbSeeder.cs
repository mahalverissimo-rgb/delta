using SGE.Data;
using SGE.Models;
using SGE.Models.Enums;

namespace SGE.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Verifica se já existem dados no banco
            if (context.Locais.Any() || context.TiposServico.Any() || context.Cardapios.Any())
            {
                return; // Banco já foi populado
            }

            // Adiciona locais padrão
            var locais = new List<Local>
            {
                new Local { Nome = "Salão Principal", Capacidade = 200, Ativo = true },
                new Local { Nome = "Salão VIP", Capacidade = 50, Ativo = true },
                new Local { Nome = "Área Externa", Capacidade = 150, Ativo = true },
                new Local { Nome = "Terraço", Capacidade = 100, Ativo = true }
            };
            context.Locais.AddRange(locais);

            // Adiciona tipos de serviço padrão
            var tiposServico = new List<TipoServico>
            {
                new TipoServico { Nome = "Buffet Completo", Descricao = "Serviço completo de buffet com garçons", Ativo = true },
                new TipoServico { Nome = "Self-Service", Descricao = "Serviço no qual os convidados se servem", Ativo = true },
                new TipoServico { Nome = "À la carte", Descricao = "Serviço com menu fixo servido à mesa", Ativo = true },
                new TipoServico { Nome = "Finger Food", Descricao = "Serviço com pequenos aperitivos servidos pelos garçons", Ativo = true }
            };
            context.TiposServico.AddRange(tiposServico);

            // Adiciona cardápios padrão
            var cardapios = new List<Cardapio>
            {
                new Cardapio { Nome = "Cardápio Executivo", Descricao = "Cardápio para eventos corporativos", Ativo = true },
                new Cardapio { Nome = "Cardápio Festa", Descricao = "Cardápio para festas e celebrações", Ativo = true },
                new Cardapio { Nome = "Cardápio Gourmet", Descricao = "Cardápio com opções gourmet", Ativo = true },
                new Cardapio { Nome = "Cardápio Vegetariano", Descricao = "Cardápio com opções vegetarianas", Ativo = true }
            };
            context.Cardapios.AddRange(cardapios);

            // Salva as alterações no banco
            context.SaveChanges();
        }
    }
}
