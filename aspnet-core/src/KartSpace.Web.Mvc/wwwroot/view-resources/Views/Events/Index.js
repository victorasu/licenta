(function ($) {
    var _eventService = abp.services.app.event,
        l = abp.localization.getSource('KartSpace'),
        _$modal = $('#EventCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#EventsTable');

    var _$eventsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _eventService.getAll,
            inputFilter: function () {
                return $('#EventsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$eventsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    if (abp.auth.isGranted('Pages.Events.Management')) {
                        return [
                            `<div class="card">`,
                            `  <div class="card-header">`,
                            `    <h3 class="card-title">Default Card Example</h3>`,
                            `    <div class="card-tools">`,
                            `      <span class="badge badge-primary">Label</span>`,
                            `    </div>`,
                            `  </div>`,
                            `  <div class="card-body">`,
                            `    The body of the card`,
                            `  </div>`,
                            `  <div class="card-footer">`,
                            `    The footer of the card`,
                            `  </div>`,
                            `</div>`
                        ].join('');
                    }
                    else {
                        return [
                            `<div class="card">`,
                            `  <div class="card-header">`,
                            `    <h3 class="card-title">Default Card Example</h3>`,
                            `    <div class="card-tools">`,
                            `      <span class="badge badge-primary">Label</span>`,
                            `    </div>`,
                            `  </div>`,
                            `  <div class="card-body">`,
                            `    The body of the card`,
                            `  </div>`,
                            `</div>`
                        ].join('');
                    }
                    
                }
            }
        ]
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var event = _$form.serializeFormToObject();


        abp.ui.setBusy(_$modal);
        _eventService
            .create(event)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$eventsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-event', function () {
        var eventId = $(this).attr("data-event-id");
        var eventName = $(this).attr('data-event-name');

        deleteEvent(eventId, eventName);
    });

    $(document).on('click', '.edit-event', function (e) {
        var eventId = $(this).attr("data-event-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Events/EditModal?eventId=' + eventId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#EventEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('event.edited', (data) => {
        _$eventsTable.ajax.reload();
    });

    function deleteEvent(eventId, eventName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                eventName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _eventService.delete({
                        id: eventId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$eventsTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$eventsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$eventsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
