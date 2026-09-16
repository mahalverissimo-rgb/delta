// Funções utilitárias
function formatMoney(value) {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(value);
}

function formatDate(date) {
    return new Intl.DateTimeFormat('pt-BR').format(new Date(date));
}

function formatDateTime(date) {
    return new Intl.DateTimeFormat('pt-BR', {
        dateStyle: 'short',
        timeStyle: 'short'
    }).format(new Date(date));
}

// Máscaras de input
$(document).ready(function () {
    // Máscara para telefone
    $('.phone-mask').mask('(00) 00000-0000');

    // Máscara para CPF
    $('.cpf-mask').mask('000.000.000-00');

    // Máscara para CNPJ
    $('.cnpj-mask').mask('00.000.000/0000-00');

    // Máscara para CEP
    $('.cep-mask').mask('00000-000');

    // Máscara para dinheiro
    $('.money-mask').mask('#.##0,00', {
        reverse: true,
        placeholder: '0,00'
    });

    // Máscara para data
    $('.date-mask').mask('00/00/0000');

    // Máscara para hora
    $('.time-mask').mask('00:00');
});

// Confirmação de exclusão
function confirmDelete(event) {
    if (!confirm('Tem certeza que deseja excluir este item?')) {
        event.preventDefault();
        return false;
    }
    return true;
}

// Inicialização de tooltips
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Inicialização de popovers
$(function () {
    $('[data-toggle="popover"]').popover();
});

// Fechar alertas automaticamente
$(document).ready(function () {
    window.setTimeout(function () {
        $('.alert-dismissible').fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 4000);
});

// Função para carregar dados via AJAX
function loadData(url, target) {
    $.get(url)
        .done(function (data) {
            $(target).html(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error('Erro ao carregar dados:', errorThrown);
            $(target).html('<div class="alert alert-danger">Erro ao carregar dados</div>');
        });
}

// Função para enviar formulário via AJAX
function submitForm(form, successCallback, errorCallback) {
    var $form = $(form);
    $.ajax({
        url: $form.attr('action'),
        type: $form.attr('method'),
        data: $form.serialize(),
        success: function (result) {
            if (typeof successCallback === 'function') {
                successCallback(result);
            }
        },
        error: function (xhr, status, error) {
            if (typeof errorCallback === 'function') {
                errorCallback(xhr, status, error);
            } else {
                console.error('Erro ao enviar formulário:', error);
            }
        }
    });
}

// Função para carregar select via AJAX
function loadSelect(url, select, valueField, textField, defaultOption) {
    var $select = $(select);
    $select.empty();

    if (defaultOption) {
        $select.append($('<option>', {
            value: '',
            text: defaultOption
        }));
    }

    $.get(url)
        .done(function (data) {
            $.each(data, function (i, item) {
                $select.append($('<option>', {
                    value: item[valueField],
                    text: item[textField]
                }));
            });
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error('Erro ao carregar select:', errorThrown);
        });
}

// Função para mostrar/esconder loader
function toggleLoader(show) {
    if (show) {
        if ($('#loader').length === 0) {
            $('body').append('<div id="loader" class="overlay"><i class="fas fa-spinner fa-spin"></i></div>');
        }
    } else {
        $('#loader').remove();
    }
}

// Função para exibir mensagem de sucesso
function showSuccess(message) {
    var alert = $('<div class="alert alert-success alert-dismissible fade show" role="alert">' +
        message +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
        '<span aria-hidden="true">&times;</span>' +
        '</button>' +
        '</div>');

    $('#alerts-container').append(alert);

    window.setTimeout(function () {
        alert.fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 4000);
}

// Função para exibir mensagem de erro
function showError(message) {
    var alert = $('<div class="alert alert-danger alert-dismissible fade show" role="alert">' +
        message +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
        '<span aria-hidden="true">&times;</span>' +
        '</button>' +
        '</div>');

    $('#alerts-container').append(alert);
}
