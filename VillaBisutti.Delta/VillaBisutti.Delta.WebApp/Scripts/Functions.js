function SetTooltip() {
	//Para Mobile e Tablets o baguiu é loco e o processo é lento
	if ($(window).width() > 991)
		$('[data-rel=tooltip]').tooltip();
}
function KeepAlive() {
	$div = $("<div/>").attr("id", "keepAlive").css("position", "absolute").css("left", "250px").css("top", "250px").css("width", "350");
	$("body").append($div);
	var url = (window.location.origin ? window.location.origin : window.location.protocol + '/' + window.location.host) + "/Home/KeepAlive";
	$div.load(url, function (response, status, xhr) {
		window.setTimeout("KeepAlive();", 300000);
		$div.remove();
	});
}
function LoadPage(url, target) {
	target = target.replace("#", "") == target ? "#" + target : target;
	var URL = url.indexOf("?") >= 0 ? "&" : "?";
	URL = url + URL + "sid=" + Math.random();
	$(target).load(URL, function (response, status, xhr) {
		HandleResponse(response, status, xhr.status, xhr.statusText, target);
	});
}
function ShowPopUp(url, title, w, h) {
	$("#PopUp").remove();
	var zIndex = GetTopMostIndex();
	var $popUpContainer = $("<div/>").attr("id", "PopUp").attr("tabindex", "-1").attr("role", "dialog").attr("aria-labelledby", "myModalLabel").addClass("modal fade").css("z-index", zIndex + 2);
	var $popUpDialog = $("<div/>").attr("role", "document").addClass("modal-dialog");
	var $popUpContent = $("<div/>").addClass("modal-content");
	var $popUpHeader = $("<div/>").addClass("modal-header");
	var $popUpCloseButton = $("<button/>").attr("type", "button").attr("data-dismiss", "modal").attr("aria-label", "Close").addClass("close white").html("<span aria-hidden=\"true\">&times;</span>");
	var $popUpHeaderText = $("<h4/>").addClass("modal-title");
	$popUpHeader.append($popUpCloseButton);
	$popUpHeader.append($popUpHeaderText);
	var $popUpBody = $("<div/>").attr("id", "PopUp_body").addClass("modal-body");
	$popUpContent.append($popUpHeader);
	$popUpContent.append($popUpBody);
	$popUpDialog.append($popUpContent);
	$popUpContainer.append($popUpDialog);
	$("body").append($popUpContainer);

	title = title ? title : 'Atenção';
	var pw = !w || isNaN(w) ? $(window).width() - 250 : w;
	var ph = !h || isNaN(h) ? $(window).height() - 250 : h;
	var URL = url.indexOf("?") >= 0 ? "&" : "?";
	URL = url + URL + "sid=" + Math.random();

	$('.modal-title').text(title);
	$('.modal-dialog').css('width', pw + 'px');
	$('.modal-body').css('height', ph + 'px');

	$('.modal-body').load(URL, function (response, status, xhr) {
		HandleResponse(response, status, xhr.status, xhr.statusText, "#PopUp_body");
	});
	$("#PopUp").modal('show');
	$('.modal-backdrop').css("z-index", zIndex);
	$('.modal-dialog').css("z-index", zIndex + 3);
}
function ShowPopUp_2(url, title, w, h) {
	var $div = $("<div/>").attr("id", "PopUp");
	$("body").append($div);
	var URL = url.indexOf("?") >= 0 ? "&" : "?";
	URL = url + URL + "sid=" + Math.random();
	var pw = !w || isNaN(w) ? $(window).width() - 100 : w;
	var ph = !h || isNaN(h) ? $(window).height() - 100 : h;
	title = !title ? "Atenção" : title;
	$("#PopUp").attr("title", title).dialog({
		modal: true,
		width: pw,
		height: ph,
		resizable: false,
		draggable: false,
		position: {
			of: $(window),
			my: "center center",
			at: "center center"
		},
		close: function (event, ui) {
			$("#PopUp").remove();
		}
	}).load(URL, function (response, status, xhr) {
		HandleResponse(response, status, xhr.status, xhr.statusText, "#PopUp");
	}).css("overflow", "auto");
}
function ClosePopUp() {
	$("#PopUp").modal('close');
}
function HandleResponse(responseText, statusResponse, statusCode, statusText, containerId) {
	if (statusResponse == "error") {
		responseText = responseText.split("<body bgcolor=\"white\">")[1];
		var id = "ErrorDetail_" + Math.random();
		id = id.replace(".", "");
		var $errorContent = $("<div/>")
					.attr("id", id)
					.append(responseText)
					.addClass("ui-helper-hidden");
		id = "#" + id;
		var $header = $("<div/>")
					.addClass("ui-state-highlight")
					.css("cursor", "pointer")
					.append("<p>")
					.append("<h3>Ihhh! xalefaram o servidor...</h3>")
					.append("Tente novamente mais tarde ou clique para ver os detalhes do erro.<br />")
					.append("Código do erro: " + statusCode + " | Resposta: " + statusText + ".")
					.append("</p>")
					.click(function () {
						if ($(id).hasClass("ui-helper-hidden")) {
							$(id).removeClass("ui-helper-hidden");
						} else {
							$(id).addClass("ui-helper-hidden");
						}
					});
		$(containerId).html("").css("overflow", "auto").append($header).append($errorContent);
		if ($(containerId).hasClass("ui-helper-hidden")) {
			$(containerId).removeClass("ui-helper-hidden");
		}
	}
}
function MostrarAlerta(text, title) {
	$("#PopUp").remove();
	var zIndex = GetTopMostIndex();
	var $popUpContainer = $("<div/>").attr("id", "PopUp").attr("tabindex", "-1").attr("role", "dialog").attr("aria-labelledby", "myModalLabel").addClass("modal fade").css("z-index", zIndex + 2);
	var $popUpDialog = $("<div/>").attr("role", "document").addClass("modal-dialog");
	var $popUpContent = $("<div/>").addClass("modal-content");
	var $popUpHeader = $("<div/>").addClass("modal-header");
	var $popUpCloseButton = $("<button/>").attr("type", "button").attr("data-dismiss", "modal").attr("aria-label", "Close").addClass("close white").html("<span aria-hidden=\"true\">&times;</span>");
	var $popUpHeaderText = $("<h4/>").addClass("modal-title");
	$popUpHeader.append($popUpCloseButton);
	$popUpHeader.append($popUpHeaderText);
	var $popUpBody = $("<div/>").attr("id", "PopUp_body").addClass("modal-body");
	$popUpContent.append($popUpHeader);
	$popUpContent.append($popUpBody);
	$popUpDialog.append($popUpContent);
	$popUpContainer.append($popUpDialog);
	$("body").append($popUpContainer);

	var $btn = $("<p id=\"OkBtn\" align=\"center\"/>")
		.addClass("col-xs-12 center-block");

	title = title ? title : 'Atenção';
	$('.modal-title').text(title);
	$('.modal-dialog').css('width', '550px');
	$('.modal-body').css('height', '108px');

	$('.modal-body').html(text + "<br />").append($btn);
	$("#OkBtn").html("<a href=\"javascript:void(0);\" data-dismiss=\"modal\" aria-label=\"Close\" class=\"btn btn-xs btn-info\"><i class=\"fa fa-thumbs-up\"></i>OK</a>");
	$("#PopUp").modal('show');
	$('.modal-backdrop').css("z-index", zIndex);
	$('.modal-dialog').css("z-index", zIndex + 3);
}
function GetTopMostIndex() {
	return Math.max(0, Math.max.apply(null, $.map($.makeArray(document.getElementsByTagName("*")),
		function (v) {
			return parseFloat($(v).css("z-index")) || null;
		})));
}
var currentY = 30;
function ValidateField(element, formId, postForm) {
	postForm = (postForm === undefined ? true : postForm);
	formId = formId.replace("#", "") == formId ? "#" + formId : formId;
	var valid = true;
	if (element.attr("data-val") == "true") {
		switch (element.attr("type")) {
			case "number":
				if (isNaN(element.val()) || element.val() == "" || element.val() < parseInt(element.attr("data-val-range-min")) || element.val() > parseInt(element.attr("data-val-range-max"))) {
					element.focus();
					AddPopOver(element, "Ooooops", element.attr("data-val-range"))
					valid = false;
					break;
				}
				break;
			case "text":
				if (element.val() == "") {
					element.val(this.defaultValue);
					element.focus();
					AddPopOver(element, "Ooooops", element.attr("data-val-required"))
					valid = false;
					break;
				}
				break;
			case "select-one":
				if (element.find("option:selected").length <= 0 || element.find("option:selected").val() == "") {
					element.focus();
					AddPopOver(element, "Ooooops", element.attr("data-val-required"))
					valid = false;
					break;
				}
				break;
		}
	}
	if (valid && postForm)
		$(formId).submit();
}
function LockPage() {
	var title = "Recurso bloqueado";
	var message = "<p>Desculpem, mas o seu perfil não deixa você acessar estas áreas / ações.<h5><i class=\"fa fa-frown-o\"></i></h5>Não fiquem bravos com o sistema que a culpa não é dele, tá?";
	$('input', 'form').attr("disabled", "disabled");
	$('select', 'form').attr("disabled", "disabled");
	$('button', 'form').attr("disabled", "disabled");
	$('.PopUpActionLinks', 'form').unbind("click")
		.attr("href", "javascript:void(0);")
		.click(function (e) {
			e.preventDefault();
			AddError(title, message);
		});
	$('.PopUpActionLinks').unbind("click")
		.attr("href", "javascript:void(0);")
		.click(function (e) {
			e.preventDefault();
			AddError(title, message);
		});
	$('.PopUpItemLinks').unbind("click")
		.attr("href", "javascript:void(0);")
		.click(function (e) {
			e.preventDefault();
			AddError(title, message);
		});
	$("a[id^='Remover']").unbind("click")
			.attr("href", "javascript:void(0);")
		.click(function (e) {
			e.preventDefault();
			AddError(title, message);
		});
	$("#PainelTipoComida li").draggable({ disabled: true });
	$("#PainelCardapios ol").droppable({ disabled: true });
	$("#SortItems").sortable("disable");
}
function HandleCheckbox(elementId) {
	elementId = elementId.replace("#", "") == elementId ? "#" + elementId : elementId;
	$(elementId + " input[type='checkbox']").each(function () {
		if ($(this).hasClass('changed'))
			return false;

		$(this).addClass('changed');

		$(this).addClass('hide');
		var $label = $('label[for=' + $(this).attr('name') + ']');
		var texto = $label.text();
		$label.remove();
		var $checkboxContainer = $("<span/>").addClass("button-checkbox");
		var $checkboxButton = $("<button/>")
			.attr("data-color", "primary")
			.attr("type", "button").text(texto)
			.addClass("btn btn-xs borderless btn-primary active");
		var $checkboxIcon = $("<i/>");

		$checkboxContainer.insertBefore($(this));
		$checkboxContainer.append($checkboxButton);
		$checkboxContainer.append($checkboxIcon);
		$checkboxContainer.append($(this));
	});
	$('.button-checkbox').each(function () {
		// Settings
		var $widget = $(this),
			$button = $widget.find('button'),
			$checkbox = $widget.find('input:checkbox'),
			color = $button.data('color'),
			settings = {
				on: {
					icon: 'glyphicon glyphicon-check'
				},
				off: {
					icon: 'glyphicon glyphicon-unchecked'
				}
			};

		// Event Handlers
		$button.on('click', function () {
			$checkbox.prop('checked', !$checkbox.is(':checked'));
			$checkbox.triggerHandler('change');
			updateDisplay();
		});
		$checkbox.on('change', function () {
			updateDisplay();
		});

		// Actions
		function updateDisplay() {
			var isChecked = $checkbox.is(':checked');

			// Set the button's state
			$button.data('state', (isChecked) ? "on" : "off");
			$checkbox.attr('checked', (isChecked) ? true : false);

			// Set the button's icon
			$button.find('.state-icon')
				.removeClass()
				.addClass('state-icon ' + settings[$button.data('state')].icon);

			// Update the button's color
			if (isChecked) {
				$button
					.removeClass('btn-white')
					.addClass('btn-' + color + ' active');
			}
			else {
				$button
					.removeClass('btn-' + color + ' active')
					.addClass('btn-white');
			}
		}

		// Initialization
		function init() {

			updateDisplay();

			// Inject the icon if applicable
			if ($button.find('.state-icon').length == 0) {
				$button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
			}
		}
		init();
	});

}
function AddPopOver(element, title, text, popOverType) {
	popOverType = (popOverType === undefined ? "popover-error" : popOverType);
	element.addClass(popOverType)
		.attr("data-rel", "popover")
		.attr("data-toggle", "popover")
		.attr("data-placement", "bottom")
		.attr("data-original-title", title)
		.attr("data-content", text)
		.popover()
		.click();
}
function AddStack(title, text) {
	$.gritter.add({
		title: title,
		text: text,
		class_name: 'gritter-info gritter-light'
	});
}
function AddError(title, text) {
	$.gritter.add({
		title: title,
		text: text,
		time: 10000,
		class_name: 'gritter-error gritter-light'
	});
}
function CreateHorarioEditor(itemId) {
	var item = itemId.replace("#", "") == itemId ? "#" + itemId : itemId;
	if (isNaN($(item).val()) || $(item).val() == "") {
		$(item).val(0);
	}
	$(item).addClass("hide");
	var $hour = $("<input/>")
		.attr("type", "number")
		.attr("min", "0")
		.attr("max", "23")
		.attr("id", itemId + "_h")
		.css("width", "40px")
		.keyup(function () {
			FormatTextBox(itemId + "_h", "0", 2, 23);
			ConvertHorarioBack(itemId);
		})
		.focusout(function () {
			FormatTextBox(itemId + "_h", "0", 2, 23);
			ConvertHorarioBack(itemId);
		})
		.change(function () {
			ConvertHorarioBack(itemId);
			FormatTextBox(itemId + "_h", "0", 2, 23);
		});
	var $minute = $("<input/>")
		.attr("type", "number")
		.attr("min", "0")
		.attr("max", "59")
		.attr("step", "5")
		.attr("id", itemId + "_m")
		.css("width", "40px")
		.keyup(function () {
			FormatTextBox(itemId + "_m", "0", 2, 59);
			ConvertHorarioBack(itemId);
		})
		.focusout(function () {
			FormatTextBox(itemId + "_m", "0", 2, 59);
		})
		.change(function () {
			ConvertHorarioBack(itemId);
			FormatTextBox(itemId + "_m", "0", 2, 59);
		});
	var $texto = $("<span/>").html(":");
	$hour.insertBefore($(item));
	$texto.insertBefore($(item));
	$minute.insertBefore($(item));
	ConvertHorario(item);
	FormatTextBox(item + "_h", "0", 2, 23);
	FormatTextBox(item + "_m", "0", 2, 59);
}
function ConvertHorario(itemId) {
	itemId = itemId.replace("#", "") == itemId ? "#" + itemId : itemId;
	var horas = Math.floor($(itemId).val() / 60);
	var minutos = $(itemId).val() - (horas * 60);
	if (horas > 23)	{
		horas = horas - 24;
	}
	$(itemId + "_h").val(horas);
	$(itemId + "_m").val(minutos);
}
function ConvertHorarioBack(itemId) {
	itemId = itemId.replace("#", "") == itemId ? "#" + itemId : itemId;
	var horas = parseInt($(itemId + "_h").val());
	var minutos = parseInt($(itemId + "_m").val());
	$(itemId).val((horas * 60) + minutos);
}
function PreventNegativeNumbers(itemId) {
	itemId = itemId.replace("#", "") == itemId ? "#" + itemId : itemId;
	$(itemId).change(function () {
		var min = parseInt($(itemId).attr("data-val-range-min"));
		var max = parseInt($(itemId).attr("data-val-range-max"));
		if ($(itemId).val() < min) {
			$(itemId).val(min);
		}
		if ($(itemId).val() > max) {
			$(itemId).val(max);
		}
	});
}
function FormatTextBox(itemId, char, len, limit) {
	itemId = itemId.replace("#", "") == itemId ? "#" + itemId : itemId;
	var txt = parseInt($(itemId).val()) + "";
	if (isNaN(txt))
		txt = "0";
	if (parseInt(txt) > limit)
		txt = limit;
	while (txt.length < len)
		txt = char + txt;
	if (txt.length > len)
		txt = txt.substr(0, len);
	$(itemId).val(txt);
}
function InitializeLoading() {
	var $background = $("<div/>")
		.attr("id", "LoadingOverlay")
		.css("z-index", GetTopMostIndex())
		.addClass("ui-widget-overlay")
		.hide();
	var $container = $("<div/>")
		.attr("id", "LoadingImage")
		.css("position", "absolute")
		.css("z-index", GetTopMostIndex())
		.position({
			of: $(window),
			my: "center center",
			at: "center center"
		})
		.append("<img src=\"" + document.location.protocol + "//" + document.location.host + "/Content/Images/ajax-loader.gif\" alt=\"Página carregando\" />")
		.hide();
	$("body").append($background);
	$("body").append($container);
}
function PreventDoubleClick() {
	$("form").submit(function () {
		$(this).submit(function (e) {
			e.preventDefault();
		});
	});
}
function ShowLoading() {
	$("#LoadingOverlay").show();
	$("#LoadingImage").show();
}
function HideLoading() {
	window.setTimeout("$(\"#LoadingOverlay\").hide();$(\"#LoadingImage\").hide();", 500);
}
$(document)
	.ajaxStart(function () {
		ShowLoading();
	})
	.ajaxStop(function () {
		HideLoading();
	})
	.ready(function () {
		InitializeLoading();
		ShowLoading();
		HideLoading();
		HandleCheckbox("main-container");
		KeepAlive();
		SetTooltip();
		PreventDoubleClick();
		$(window).resize(function () { window.location.reload(false) }); //Caso a browser seja redimensionado, a página será recarregada para que seus ítens sejam ajustados
	})
	.error(function () {
		HideLoading();
	});
$.extend($.gritter.options, {
	class_name: 'gritter-light', // for light notifications (can be added directly to $.gritter.add too)
	position: 'top-right', // possibilities: bottom-left, bottom-right, top-left, top-right
	fade_in_speed: 100, // how fast notifications fade in (string or int)
	fade_out_speed: 100, // how fast the notices fade out
	time: 1000 // hang on the screen for...
});
