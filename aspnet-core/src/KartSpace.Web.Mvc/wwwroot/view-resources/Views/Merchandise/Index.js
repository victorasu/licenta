(function ($) {
    var _merchService = abp.services.app.merch,
        l = abp.localization.getSource('KartSpace'),
        _$modal = $('#MerchCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#MerchTable');

    var _$merchTable = _$table.DataTable({
        language: {
            url: `//cdn.datatables.net/plug-ins/1.12.1/i18n/${abp.localization.currentLanguage.name}.json`
        },
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var input = $('#MerchandiseSearchForm').serializeFormToObject(true);
            var tipMerch = $('#TipMerch').val();
            input.maxResultCount = data.length;
            input.skipCount = data.start;

            _merchService.getMerchList(input, tipMerch).done(function (result) {
                callback({
                    data: result.items,
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount
                });
            }).always(function () {
                abp.ui.clearBusy(_$merchTable);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$merchTable.draw(false)
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
                    if (abp.auth.isGranted('Pages.Merch.Management')) {
;                        return [
                            `<div class="card">`,
                            `  <div class="card-header">`,
                            `    <h3 class="card-title">${row.name}</h3>`,
                            `    <div class="card-tools">`,
                            `    <button type="button" class="btn btn-sm bg-secondary edit-merch" data-merch-id="${row.id}" data-toggle="modal" data-target="#MerchEditModal">`,
                            `        <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                            '    </button>',
                            `    <button type="button" class="btn btn-sm bg-danger delete-merch" data-merch-id="${row.id}" data-merch-name="${row.name}">`,
                            `        <i class="fas fa-trash"></i> ${l('Delete')}`,
                            '    </button>',
                            `    </div>`,
                            `  </div>`,
                            `  <div class="card-body">`,
                            `    ${row.description}`,
                            `  </div>`,
                            `  <div class="card-footer">`,
                            `      <span class="badge badge-primary">${row.price}$</span> <span class="badge badge-secondary">${row.categoryName}</span>`,
                            `  </div>`,
                            `</div>`
                        ].join('');
                    }
                    else {
                        if (abp.session.multiTenancySide == 2) {
                            return [
                                `<div class="card">`,
                                `  <div class="card-header">`,
                                `    <h3 class="card-title">${row.name}</h3>`,
                                `    <div class="card-tools">`,
                                `    <button type="button" class="btn btn-sm bg-gradient-info buy-merch" data-merch-id="${row.id}" data-toggle="modal" data-target="#MerchBuyModal">`,
                                `        <i class="fas fa-cart-plus"></i> ${l('Buy')}`,
                                '    </button>',
                                `    </div>`,
                                `  </div>`,
                                `  <div class="card-body">`,
                                `    ${row.description}`,
                                `  </div>`,
                                `  <div class="card-footer">`,
                                `      <span class="badge badge-primary">${row.price}$</span> <span class="badge badge-secondary">${row.categoryName}</span>`,
                                `  </div>`,
                                `</div>`
                            ].join('');
                        }
                        else {
                            return [
                                `<div class="card">`,
                                `  <div class="card-header">`,
                                `    <h3 class="card-title">${row.name}</h3>`,
                                `    <div class="card-tools">`,
                                `      <span class="badge badge-primary">${row.price}$</span> <span class="badge badge-secondary">${row.categoryName}</span>`,
                                `    </div>`,
                                `  </div>`,
                                `  <div class="card-body">`,
                                `    ${row.description}`,
                                `  </div>`,
                                `</div>`
                            ].join('');
                        }
                    }
                }
            }
        ]
    });

    $("#TipMerch").on('change', (e) => {
        _$merchTable.ajax.reload();
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var merch = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _merchService
            .create(merch)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$merchTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-merch', function () {
        var merchId = $(this).attr("data-merch-id");
        var merchName = $(this).attr('data-merch-name');

        deleteMerch(merchId, merchName);
    });

    $(document).on('click', '.edit-merch', function (e) {
        var merchId = $(this).attr("data-merch-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Merchandise/EditModal?merchId=' + merchId,
            type: 'POST',
            dataType: 'html',
            success: function(content) {
                $('#MerchEditModal div.modal-content').html(content);
            },
            error: function(e) {
            }
        });
    });

    $(document).on('click', '.buy-merch', function (e) {
        var merchId = $(this).attr("data-merch-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Merchandise/BuyModal?merchId=' + merchId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#MerchBuyModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    abp.event.on('merch.edited', (data) => {
        _$merchTable.ajax.reload();
    });

    function deleteMerch(merchId, merchName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                merchName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _merchService.delete({
                        id: merchId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$merchTable.ajax.reload();
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
        _$merchTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$merchTable.ajax.reload();
            return false;
        }
    });

})(jQuery);
