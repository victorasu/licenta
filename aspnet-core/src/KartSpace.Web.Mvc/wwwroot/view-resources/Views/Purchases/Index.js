(function ($) {
    var _purchaseService = abp.services.app.purchase,
        l = abp.localization.getSource('KartSpace'),
        _$table = $('#PurchasesTable');

    var _$purchaseTable = _$table.DataTable({
        language: {
            url: `//cdn.datatables.net/plug-ins/1.12.1/i18n/${abp.localization.currentLanguage.name}.json`
        },
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var input = $('#PurchasesSearchForm').serializeFormToObject(true);
            var stareComanda = $('#StareComanda').val();
            input.maxResultCount = data.length;
            input.skipCount = data.start;

            _purchaseService.displayPurchases(input, stareComanda).done(function (result) {
                callback({
                    data: result.items,
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount
                });
            }).always(function () {
                abp.ui.clearBusy(_$purchaseTable);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$purchaseTable.draw(false)
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
                data: 'productName',
                sortable: false
            },
            {
                targets: 2,
                data: 'price',
                sortable: false
            },
            {
                targets: 3,
                data: 'userFullName',
                sortable: false
            },
            {
                targets: 4,
                data: 'userPhoneNumber',
                sortable: false,
            },
            {
                targets: 5,
                data: 'creationDateString',
                sortable: false
            },
            {
                targets: 6,
                data: 'stareComandaName',
                sortable: false,
            },
            {
                targets: 7,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    if (abp.auth.isGranted('Pages.Merch.Management')) {
                        return [
                            `   <button type="button" class="btn btn-sm bg-dark edit-state" data-purchase-id="${row.id
                            }" data-toggle="modal" data-target="#StateEditModal">`,
                            `       <i class="fas fa-pencil-alt"></i> ${l('EditState')}`,
                            '   </button>'
                        ].join('');
                    } else {
                        return [' '].join('');
                    }
                }
            }
        ]
    });


    $("#StareComanda").on('change', (e) => {
        _$purchaseTable.ajax.reload();
    });

    $(document).on('click', '.edit-state', function (e) {
        var purchaseId = $(this).attr("data-purchase-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Purchases/EditStareModal?purchaseId=' + purchaseId,
            type: 'GET',
            dataType: 'html',
            success: function(content) {
                $('#StateEditModal div.modal-content').html(content);
            },
            error: function(e) {
            }
        });
    });

    abp.event.on('purchase.edited', (data) => {
        _$purchaseTable.ajax.reload();
    });

    $('.btn-search').on('click', (e) => {
        _$purchaseTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$purchaseTable.ajax.reload();
            return false;
        }
    });

    var _$recTable = $('#RecommendationTable');

    var _recTable = _$recTable.DataTable({
        paging: false,
        ordering: false,
        info: false,
        serverSide: true,
        listAction: {
            ajaxFunction: _purchaseService.getRecommendations
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _recTable.draw(false)
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
                defaultContent: ''
            },
            {
                targets: 1,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [`<div class="card">`,
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
        ]
    });
})(jQuery);
