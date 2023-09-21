function regionController() {
    this.page = 1;
    this.totalPage = 1;
    this.initializer = () => {
        registerEvents();
        loadRegions();
    }

    const registerEvents = () => {
        $('.region.label-filter').on('click', (e) => {
            const target = $(e.target);

            if (target.hasClass('active')) {
                target.removeClass('active');
                $('.label-filter').first().addClass('active');
            }
            else {
                $('.label-filter').removeClass('active');
                target.addClass('active');
            }

            loadRegions();
        });

        $('#cbIncludeInActiveRegion').on('change', () => {
            loadRegions();
        })

        $('.txt-pageNumber.region').on('keydown', (e) => {
            if (!common.configs.keycodes.includes(e.keyCode))
                e.preventDefault();
        })

        $('.txt-pageNumber.region').on('keyup', (e) => {
            if (e.keyCode == common.configs.keys.Enter) {
                let isOk = true;

                if ($(e.target).val() == "" || $(e.target).val() == "0") {
                    $(e.target).val(1);
                    isOk = false;
                }

                if (isOk)
                    loadRegions();
            }
            else
                this.page = parseInt(e.target.value);
        })

        $('#btnFirstPageRegion').on('click', (e) => {
            if (this.page > 1) {
                this.page = 1;
                $('.txt-pageNumber').val(1);

                loadRegions();
            }
        })

        $('#btnPrevPageRegion').on('click', (e) => {
            if (this.page > 1) {
                this.page -= 1;
                $('.txt-pageNumber').val(this.page);

                loadRegions();
            }
        })

        $('#btnLastPageRegion').on('click', (e) => {
            this.page = this.totalPage;
            $('.txt-pageNumber').val(this.page);

            loadRegions();
        })

        $('#btnNextPageRegion').on('click', (e) => {
            if (this.page < this.totalPage) {
                this.page += 1;
                $('.txt-pageNumber').val(this.page);

                loadRegions();
            }
        })

        $(document).on('click', '.row-data-region', (e) => {
            e.preventDefault();

            const row = $(e.target).closest('.row-data-region');
            const id = parseInt(row.attr('data-id'));
            const isActive = row.attr('data-active') == "true" ? true : false;

            if (!isActive) {
                common.confirm("Do you want to make this Trust Region active?", () => {
                    activeRegion(id)
                });
            }
        })

        $(document).on('click', 'td.col-region-name .nav-link', (e) => {
            e.preventDefault();

            const row = $(e.target).closest('tr.row-data-region');
            const regionId = parseInt(row.attr('data-id')) || 0;
            const isActive = row.attr('data-active') == "true" ? true : false;

            if (isActive && regionId > 0) {
                loadCountries();
                setTimeout(() => {
                    getRegionDetail(regionId, () => { openRegionModal('update', regionId);});
                }, 300)
            }
        })

        $('#btnCreateRegion').on('click', () => {
            openRegionModal('create');
            loadCountries();
        });

        $('#btnInActiveRegion').on('click', () =>                    {
            const regionId = parseInt($('input#regionId').val());
            inActiveRegion(regionId);
            refreshRegionModal();
            closeRegionModal();
        })

        $('.modal .form-control').on('click', (e) => {
            $(e.target).removeClass('error');
        })

        $('#btnSaveRegion').on('click', (e) => {
            e.preventDefault();

            let isValid = true;

            const country = parseInt($('#regionModal #countryId').val()) || 0;
            const regionName = $('#regionModal #regionName').val();
            const description = $('#regionModal #description').val();

            const regionId = parseInt($('#regionModal #regionId').val()) || 0;

            if (!validateCountry(country))
                isValid = false;
            else
                hideCountryErrorMessage();

            if (!validateRegionName(regionName))
                isValid = false;
            else
                hideRegionNameErrorMessage();

            if (isValid) {
                if (regionId === 0)
                    createRegion(country, regionName, description);
                else
                    updateRegion(regionId, country, regionName, description)
            }
        })

        $('#btnBackRegion').on('click', () => {
            closeRegionModal();
            refreshRegionModal();
        })

        $('#btnInActiveRegion').on('click', () => {
            const regionId = 0;
            if (regionId >= 1)
                inActiveRegion(regionId);
        })
    }

    const showCountryError = (message) => {
        $('#country').addClass('error');
        $('.modal .country-error').html(message);
    }

    const hideCountryErrorMessage = () => {
        $('.modal .country-error').html('');
    }

    const showRegionNameError = (message) => {
        $('#regionName').addClass('error');
        $('.modal .regionName-error').html(message);
    }

    const hideRegionNameErrorMessage = () => {
        $('.modal .regionName-error').html('');
    }

    const refreshRegionModal = () => {
        const country = $('#country');
        const regionName = $('#regionName');
        const descrition = $('#description');
        const regionId = $('#regionId');

        const countryError = $('.modal .country-error');
        const regionNameError = $('.modal .regionName-error');

        regionId.val('');

        country.val('');
        country.removeClass('error');
        countryError.html('');

        regionName.val('');
        regionName.removeClass('error');
        regionNameError.html('');

        descrition.val('');
    }

    const openRegionModal = (type, regionId) => {
        $('#regionModal').modal('show');
        if (type == 'create') {
            $('#btnInActiveRegion').parent().addClass('d-none');
            $('#regionModal .tab-districts').addClass('d-none');
            $('#btnSaveRegion').parent().addClass('ml-auto');
        }
        else {
            $('input#regionId').val(regionId);
        }
    }

    const closeRegionModal = () => {
        $('#regionModal').modal('hide');
        $('#btnInActiveRegion').parent().removeClass('d-none');
        $('#btnSaveRegion').parent().removeClass('ml-auto');

        $('.tab-districts').removeClass('d-none');

        $('.tab-details').find('.nav-link').addClass('active');
        $('.tab-content #details').addClass('active');
        $('.tab-content #details').addClass('show');

        $('.tab-districts').find('.nav-link').removeClass('active');
        $('.tab-content #trustDistricts').removeClass('active');
        $('.tab-content #trustDistricts').removeClass('show');
    }

    const validateCountry = (value) => {
        if (value == 0) {
            showCountryError('Please select the Nation/Country');
            return false;
        }

        return true;
    }

    const validateRegionName = (value) => {
        if (value.trim().length == 0) {
            showRegionNameError('Please input the Region Name');

            return false;
        }

        return true;
    }

    const generateRegionRowTemplate = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data-region" data-id="' + item.id + '" data-active="' + item.isActive + '">'
            + '<td class="col-region-name"><div class="text-left"><a class="nav-link p-0" href="/region/' + item.regionName + '">' + item.regionName + '</a></div></td>'
            + '<td><div class="text-left">' + (item.description || "") + '</div></td>'
            + '<td><div class="text-left">' + item.country.countryName + '</div></td>'
            + '<td><div class="text-left">' + (item.isActive ? "Yes" : "No") + '</div></td>'
            + '<td></td>'
            + '</tr>';
    }

    const generateRegionTableNoData = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
    }

    const loadCountries = () => {
        $.ajax({
            method: "GET",
            url: "/country/get",
            dataType: "json",
            success: (res) => {
                if (res.success) {
                    let options = '<option value="">--</option>';
                    if (res.data.length > 0) {
                        res.data.forEach((item, i) => {
                            options += '<option value="' + item.id + '">' + item.countryName + '</option>'
                        })

                        if (options.length > 0)
                            $('#countryId').html(options);
                    }
                }
            },
            error: (res) => {
                console.log(res);
            }
        })
    }

    const loadRegions = () => {
        const firstCharacters = common.getFilterName(parseInt($('.label-filter.active').attr('data-type')));
        const includeInActive = $('#cbIncludeInActiveRegion').is(":checked");
        const tbody = $('#table-regions').find('tbody');

        $.ajax({
            method: "POST",
            url: "/trustregion/paging",
            dataType: "json",
            data: {
                "page": this.page,
                "firstCharacters": firstCharacters,
                "includeInActive": includeInActive
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data.items.length > 0) {
                        res.data.items.forEach((item, i) => {
                            render += generateRegionRowTemplate(item);
                        })

                        this.page = res.data.currentPage;
                        this.totalPage = res.data.pageCount;
                        $('.txt-pageNumber.region').val(this.page);
                        $('.totalPageRegion').html(this.totalPage);

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateRegionTableNoData());
                }
                else {
                    common.notify(res.errorMessage, "error");
                }
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
                console.log(res);
            }
        })
    }

    const getRegionDetail = (regionId, callBack) => {
        $.ajax({
            url: "/trustregion/details",
            method: "POST",
            dataType: "json",
            data: {
                regionId: regionId
            },
            success: (res) => {
                if (res.success) {
                    $('#countryId').val(res.data.countryId);
                    $('#regionName').val(res.data.regionName);
                    $('#description').val(res.data.description);

                    if (res.data.districts.length > 0) {
                        let rowDistrictTemplates = '';
                        res.data.districts.forEach((item, i) => {
                            rowDistrictTemplates += district.generateDistrictRowTemplate(item);
                        })

                        if (rowDistrictTemplates)
                            $('#trustDistricts #table-districts').find('tbody').html(rowDistrictTemplates);
                    }
                    else
                        $('#trustDistricts #table-districts').find('tbody').html('');

                    if (typeof (callBack) == "function")
                        callBack();
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const createRegion = (countryId, regionName, description) => {
        $.ajax({
            method: "POST",
            url: "/trustRegion/create",
            dataType: "json",
            data: {
                CountryId: countryId,
                RegionName: regionName,
                Description: description
            },
            success: (res) => {
                if (res.success) {
                    closeRegionModal();
                    refreshRegionModal();
                    common.notify("Inserted 1 record", "success");
                    loadRegions();
                }
                else {
                    switch (res.code) {
                        case -1:
                            common.notify(res.errorMessage, "error");
                            break;
                        case 1:
                            showCountryError(res.errorMessage);
                            hideRegionNameErrorMessage();
                            break;
                        default:
                            showRegionNameError(res.errorMessage);
                            hideCountryErrorMessage();
                            break;
                    }
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }

    const updateRegion = (regionId, countryId, regionName, description) => {
        $.ajax({
            method: "POST",
            url: "/trustRegion/update",
            dataType: "json",
            data: {
                Id: regionId,
                CountryId: countryId,
                RegionName: regionName,
                Description: description
            },
            success: (res) => {
                if (res.success) {
                    closeRegionModal();
                    refreshRegionModal();
                    common.notify("Updated 1 record", "success");
                    loadRegions();
                }
                else {
                    switch (res.code) {
                        case -1:
                            common.notify(res.errorMessage, "error");
                            break;
                        case 1:
                            showCountryError(res.errorMessage);
                            hideRegionNameErrorMessage();
                            break;
                        default:
                            showRegionNameError(res.errorMessage);
                            hideCountryErrorMessage();
                            break;
                    }
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }

    const activeRegion = (regionId) => {
        $.ajax({
            url: "/trustregion/active",
            method: "POST",
            dataType: "json",
            data: {
                regionId: regionId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("successfully", "success")
                    loadRegions();
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const inActiveRegion = (regionId) => {
        $.ajax({
            url: "/trustregion/inactive",
            method: "POST",
            dataType: "json",
            data: {
                regionId: regionId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("successfully", "success")
                    loadRegions();
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }
}