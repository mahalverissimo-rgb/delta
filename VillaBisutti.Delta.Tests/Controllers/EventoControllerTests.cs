using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VillaBisutti.Delta.WebApp.Controllers;
using VillaBisutti.Delta.WebApp.Data;
using VillaBisutti.Delta.WebApp.Models;
using Xunit;

namespace VillaBisutti.Delta.Tests.Controllers
{
    public class EventoControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly EventoController _controller;

        public EventoControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            _contextMock = new Mock<ApplicationDbContext>(options);
            _controller = new EventoController(context);
        }

        [Fact]
        public async Task Index_ReturnsViewWithEventos()
        {
            // Arrange
            var eventos = new List<Evento>
            {
                new Evento { Id = 1, NomeResponsavel = "Teste 1" },
                new Evento { Id = 2, NomeResponsavel = "Teste 2" }
            };

            var dbSetMock = new Mock<DbSet<Evento>>();
            dbSetMock.As<IQueryable<Evento>>().Setup(m => m.Provider).Returns(eventos.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Evento>>().Setup(m => m.Expression).Returns(eventos.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Evento>>().Setup(m => m.ElementType).Returns(eventos.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Evento>>().Setup(m => m.GetEnumerator()).Returns(eventos.GetEnumerator());

            _contextMock.Setup(c => c.Eventos).Returns(dbSetMock.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Evento>>(result.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var evento = new Evento
            {
                NomeResponsavel = "Teste",
                EmailResponsavel = "teste@teste.com",
                TelefoneResponsavel = "(11) 99999-9999",
                Data = DateTime.Now.AddDays(1),
                Pax = 100
            };

            // Act
            var result = await _controller.Create(evento) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            var evento = new Evento(); // Evento inválido sem dados obrigatórios
            _controller.ModelState.AddModelError("", "Error");

            // Act
            var result = await _controller.Create(evento) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Edit(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenEventoNotFound()
        {
            // Act
            var result = await _controller.Edit(999) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Delete(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            // Arrange
            var evento = new Evento { Id = 1 };
            _contextMock.Setup(c => c.Eventos.FindAsync(1)).ReturnsAsync(evento);

            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
