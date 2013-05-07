(function ($, w, undefined) {
    if (w.footable == undefined || w.footable == null)
        throw new Error('Please check and make sure footable.js is included in the page and is loaded prior to this script.');

    var defaults = {
        footableDatatablesIntegrationPlugin: {
            enabled: true
        }
    };

    function FootableDatatablesIntegrationPlugin() {
        var p = this;
        p.name = 'Footable Datatables Integration Plugin';
        p.init = function (ft) {
            $(ft.table).bind({

                'footable_resized': function (e) {
                    if (e.ft.options.footableDatatablesIntegrationPlugin.enabled == true) {

                        var opt = ft.options;
                        var $table = $(ft.table);
                        $table.find(opt.columnDataSelector).each(function () {
                            var data = e.ft.getColumnData(this);
                            e.ft.columns[data.index] = data;

                            if (data.className != null) {
                                var selector = '', first = true;
                                $.each(data.matches, function (m, match) { //support for colspans
                                    if (!first) {
                                        selector += ', ';
                                    }
                                    selector += '> tbody > tr:not(.footable-row-detail) > td:nth-child(' + (parseInt(match) + 1) + ')';
                                    first = false;
                                });
                                //add the className to the cells specified by data-class="blah"
                                $table.find(selector).not('.footable-cell-detail').addClass(data.className);
                            }
                        });

                        e.ft.bindToggleSelectors();
                    }
                }                
            });
        };
    };

    w.footable.plugins.register(new FootableDatatablesIntegrationPlugin(), defaults);

})(jQuery, window);