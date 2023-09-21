function districtController() {
    this.page = 1;
    this.totalPage = 1;
    this.initializer = () => {
        registerEvents();
    }

    const registerEvents = () => {
        $('.district.label-filter').on('click', (e) => {
            const target = $(e.target);

            if (target.hasClass('active')) {
                target.removeClass('active');
                $('.district.label-filter').first().addClass('active');
            }
            else {
                $('.district.label-filter').removeClass('active');
                target.addClass('active');
            }

            loadDistricts();
        });

        $('#cbIncludeInActiveDistrict').on('change', () => {
            loadDistricts();
        })

        $('.district.txt-pageNumber').on('keydown', (e) => {
            if (!common.configs.keycodes.includes(e.keyCode))
                e.preventDefault();
        })

        $('.district.txt-pageNumber').on('keyup', (e) => {
            if (e.keyCode == common.configs.keys.Enter) {
                let isOk = true;

                if ($(e.target).val() == "" || $(e.target).val() == "0") {
                    $(e.target).val(1);
                    isOk = false;
                }

                if (isOk)
                    loadDistricts();
            }
            else
                this.page = parseInt(e.target.value);
        })

        $('#btnFirstPageDistrict').on('click', (e) => {
            if (this.page > 1) {
                this.page = 1;
                $('.district.txt-pageNumber').val(1);

                loadDistricts();
            }
        })

        $('#btnPrevPageDistrict').on('click', (e) => {
            if (this.page > 1) {
                this.page -= 1;
                $('.district.txt-pageNumber').val(this.page);

                loadDistricts();
            }
        })

        $('#btnLastPageDistrict').on('click', (e) => {
            this.page = this.totalPage;
            $('.district.txt-pageNumber').val(this.page);

            loadDistricts();
        })

        $('#btnNextPageDistrict').on('click', (e) => {
            if (this.page < this.totalPage) {
                this.page += 1;
                $('.district.txt-pageNumber').val(this.page);

                loadDistricts();
            }
        })

        $(document).on('click', '.row-data-district', (e) => {
            e.preventDefault();

            const row = $(e.target).closest('.row-data-district');
            const id = parseInt(row.attr('data-id'));
            const isActive = row.attr('data-active') == "true" ? true : false;

            if (!isActive) {
                common.confirm("Do you want to make this Trust District active?", () => {
                    activeDistrict(id)
                });
            }
        })

        $(document).on('click', '.row-data-district td.col-district-name', (e) => {
            e.preventDefault();

            const row = $(e.target).closest('tr.row-data-district');
            const districtId = parseInt(row.attr('data-id')) || 0;
            const isActive = row.attr('data-active') == "true" ? true : false;

            if (isActive && districtId > 0) {
                getDistrictDetail(districtId);
            }
        })

        $('#btnCreateDistrict').on('click', () => {
            const regionId = $('#regionId').val();
            openDistrictModal('create');
            loadRegionName(regionId);
        });

        $('#btnInActiveDistrict').on('click', () => {
            const districtId = parseInt($('#districtModal input#districtId').val());
            inActiveDistrict(districtId);
            refreshDistrictModal();
            closeDistrictModal();
        })

        $('#districtModal .form-control').on('click', (e) => {
            $(e.target).removeClass('error');
        })

        $('#btnSaveDistrict').on('click', (e) => {
            e.preventDefault();

            let isValid = true;

            const districtId = parseInt($('#districtModal #districtId').val()) || 0;
            const districtName = $('#districtModal #districtName').val();
            const description = $('#districtModal #districtDescription').val();

            const regionId = parseInt($('#districtModal #region').attr('data-id')) || 0;

            if (!validateDistrictName(districtName))
                isValid = false;
            else
                hideErrorMessage();

            if (isValid) {
                if (districtId === 0)
                    createDistrict(regionId, districtName, description);
                else
                    updateDistrict(districtId, districtName, description, regionId)
            }
        })

        $('#btnBackDistrict').on('click', () => {
            closeDistrictModal();
            refreshDistrictModal();
        })

        $('#btnInActiveDistrict').on('click', () => {
            const districtId = 0;
            if (districtId >= 1)
                inActiveDistrict(districtId);
        })
    }

    const showError = (message) => {
        $('#districtName').addClass('error');
        $('#districtModal .districtName-error').html(message);
    }

    const hideErrorMessage = () => {
        $('#districtModal .districtName-error').html('');
    }

    const refreshDistrictModal = () => {
        const region = $('#districtModal #regionId')
        const districtName = $('#districtName');
        const description = $('#districtDescription');
        const districtId = $('#districtId');

        const errorMessage = $('#districtModal .districtName-error');

        region.val('');
        region.attr('data-id', '');

        districtId.val('');

        districtName.val('');
        districtName.removeClass('error');
        errorMessage.html('');

        description.val('');
    }

    const openDistrictModal = (type, districtId) => {
        $('#districtModal').modal('show');

        if (type == 'create') {
            $('#btnInActiveDistrict').parent().addClass('d-none');
            $('#btnSaveDistrict').parent().addClass('ml-auto');
        }
        else {
            $('input#districtId').val(districtId);
        }
    }

    const closeDistrictModal = () => {
        $('#districtModal').modal('hide');
        $('#btnInActiveDistrict').parent().removeClass('d-none');
        $('#btnSaveDistrict').parent().removeClass('ml-auto');
    }

    const validateDistrictName = (value) => {
        if (value.trim().length == 0) {
            showError('Please input the District Name');

            return false;
        }

        return true;
    }

    this.generateDistrictRowTemplate = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data-district" data-id="' + item.id + '" data-active="' + item.isActive + '">'
            + '<td class="col-district-name"><div class="text-left"><a class="nav-link p-0" href="/district/' + item.districtName + '">' + item.districtName + '</a></div></td>'
            + '<td><div class="text-left">' + (item.description || "") + '</div></td>'
            + '<td><div class="text-left">' + item.region.regionName + '</div></td>'
            + '<td><div class="text-left">' + (item.isActive ? "Yes" : "No") + '</div></td>'
            + '<td></td>'
            + '</tr>';
    }

    const generateDistrictTableNoData = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
    }

    const loadRegionName = (regionId) => {
        $.ajax({
            url: "/trustregion/getregion",
            method: "GET",
            dataType: "json",
            data: {
                regionId: regionId
            },
            success: (res) => {
                $('#districtModal #region').val(res.data.regionName);
                $('#districtModal #region').attr('data-id', res.data.id);
            },
            error: (res) => {
            }
        })
    }

    const loadDistricts = () => {
        const regionId = parseInt($('#regionId').val());
        const firstCharacters = common.getFilterName(parseInt($('.district.label-filter.active').attr('data-type')));
        const includeInActive = $('#cbIncludeInActiveDistrict').is(":checked");
        const tbody = $('#table-districts').find('tbody');

        $.ajax({
            method: "POST",
            url: "/trustdistrict/Get",
            dataType: "json",
            data: {
                "page": this.page,
                "firstCharacters": firstCharacters,
                "includeInActive": includeInActive,
                "regionId": regionId
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data.items.length > 0) {
                        res.data.items.forEach((item, i) => {
                            render += this.generateDistrictRowTemplate(item);
                        })

                        this.page = res.data.currentPage;
                        this.totalPage = res.data.pageCount;
                        $('.district.txt-pageNumber').val(this.page);
                        $('.totalPageDistrict').html(this.totalPage);

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateDistrictTableNoData());
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

    const getDistrictDetail = (districtId) => {
        $.ajax({
            url: "/trustdistrict/details",
            method: "POST",
            dataType: "json",
            data: {
                districtId: districtId
            },
            success: (res) => {
                if (res.success) {
                    $('#districtModal #region').val(res.data.region.regionName);
                    $('#districtModal #region').attr('data-id', res.data.region.id);

                    $('#districtModal #districtName').val(res.data.districtName);

                    $('#districtModal #districtDescription').val(res.data.description);

                    openDistrictModal('update', districtId);
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const createDistrict = (regionId, districtName, description) => {
        $.ajax({
            method: "POST",
            url: "/trustdistrict/create",
            dataType: "json",
            data: {
                DistrictName: districtName,
                Description: description,
                Region: { Id: regionId }
            },
            success: (res) => {
                if (res.success) {
                    closeDistrictModal();
                    refreshDistrictModal();
                    common.notify("Inserted 1 record", "success");
                    loadDistricts();
                }
                else {
                    if (res.code === -1)
                        common.notify(res.errorMessage, "error");
                    else
                        showError(res.errorMessage);
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }

    const updateDistrict = (districtId, districtName, description, regionId) => {
        $.ajax({
            method: "POST",
            url: "/trustdistrict/update",
            dataType: "json",
            data: {
                Id: districtId,
                DistrictName: districtName,
                Description: description,
                Region: { Id: regionId }
            },
            success: (res) => {
                if (res.success) {
                    closeDistrictModal();
                    refreshDistrictModal();
                    common.notify("Updated 1 record", "success");
                    loadDistricts();
                }
                else {
                    if (res.code === -1)
                        common.notify(res.errorMessage, "error");
                    else
                        showError(res.errorMessage);
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }

    const activeDistrict = (districtId) => {
        $.ajax({
            url: "/trustdistrict/active",
            method: "POST",
            dataType: "json",
            data: {
                districtId: districtId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("successfully", "success")
                    loadDistricts();
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const inActiveDistrict = (districtId) => {
        $.ajax({
            url: "/trustdistrict/inactive",
            method: "POST",
            dataType: "json",
            data: {
                districtId: districtId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("successfully", "success")
                    loadDistricts();
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