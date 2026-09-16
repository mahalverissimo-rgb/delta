// dashboard.js - Script para a página inicial do SGE
$(function () {
    // Variável global para o gráfico
    var donutChart;

    // Função para inicializar o gráfico
    function initChart() {
        // Verifica se o elemento donutChart existe na página
        if ($('#donutChart').length) {
            // Obtém os dados do gráfico de donut
            var eventosCount = parseInt($('#eventosCount').val()) || 0;
            var locaisCount = parseInt($('#locaisCount').val()) || 0;
            var cardapiosCount = parseInt($('#cardapiosCount').val()) || 0;
            var servicosCount = parseInt($('#servicosCount').val()) || 0;

            // Configuração do gráfico de donut
            var donutChartCanvas = $('#donutChart').get(0).getContext('2d');
            var donutData = {
                labels: ['Eventos', 'Locais', 'Cardápios', 'Serviços'],
                datasets: [
                    {
                        data: [eventosCount, locaisCount, cardapiosCount, servicosCount],
                        backgroundColor: ['#17a2b8', '#28a745', '#ffc107', '#dc3545'],
                    }
                ]
            };
            var donutOptions = {
                maintainAspectRatio: false,
                responsive: true,
            };

            // Destruir gráfico existente se houver
            if (donutChart) {
                donutChart.destroy();
            }

            // Criar novo gráfico
            donutChart = new Chart(donutChartCanvas, {
                type: 'doughnut',
                data: donutData,
                options: donutOptions
            });
        }
    }

    // Inicializar gráfico
    initChart();

    // Botão de atualização do gráfico
    $('#refreshChart').click(function() {
        $(this).find('i').addClass('fa-spin');
        setTimeout(function() {
            $('#refreshChart').find('i').removeClass('fa-spin');
            initChart();
        }, 1000);
    });

    // Botão de atualização dos eventos
    $('#refreshEvents').click(function() {
        $(this).find('i').addClass('fa-spin');
        
        // Simulação de atualização (em produção, seria uma chamada AJAX)
        setTimeout(function() {
            $('#refreshEvents').find('i').removeClass('fa-spin');
            // Aqui você poderia fazer uma chamada AJAX para atualizar os dados
            // Por exemplo:
            // $.ajax({
            //     url: '/Home/GetLatestEvents',
            //     type: 'GET',
            //     success: function(data) {
            //         // Atualizar a tabela com os novos dados
            //     }
            // });
        }, 1000);
    });

    // Adicionar tooltips aos botões
    $('[data-toggle="tooltip"]').tooltip();
});
