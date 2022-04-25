(function ($) {
    var _eventService = abp.services.app.event,
        l = abp.localization.getSource('KartSpace'),
        _$modal = $('#EventEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var event = _$form.serializeFormToObject();
        event.grantedPermissions = [];
        var _$permissionCheckboxes = _$form[0].querySelectorAll("input[name='permission']:checked");
        if (_$permissionCheckboxes) {
            for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                event.grantedPermissions.push(_$permissionCheckbox.val());
            }
        }

        abp.ui.setBusy(_$form);
        _eventService.update(event).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('event.edited', event);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    $(function () {
        $("#EStartTime").datetimepicker({
            locale: 'ro',
            icons: {
                time: 'fa fa-clock',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: "fa fa-desktop",
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            },
            format: "MM.DD.YYYY",
            sideBySide: true,
            showTodayButton: true,
            showClose: true,
            toolbarPlacement: "bottom",
            allowInputToggle: true
        });

        $("#EEndTime").datetimepicker({
            locale: 'ro',
            icons: {
                time: 'fa fa-clock',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: "fa fa-desktop",
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            },
            format: "MM.DD.YYYY",
            sideBySide: true,
            showTodayButton: true,
            showClose: true,
            toolbarPlacement: "bottom",
            allowInputToggle: true,
            useCurrent: false
        });

        $("#EStartTime").on("dp.change", function (e) {
            $('#EEndTime').data("DateTimePicker").minDate(e.date);
        });
        $("#EEndTime").on("dp.change", function (e) {
            $('#EStartTime').data("DateTimePicker").maxDate(e.date);
        });
    });

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
