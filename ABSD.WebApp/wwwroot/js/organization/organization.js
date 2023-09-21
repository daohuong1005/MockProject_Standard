function organizationController() {
    this.page = 1;
    this.totalPage = 1;
    var servicesId = 0;
    this.initializer = () => {
        registerEvents();
    }

    const registerEvents = () => {
        $('.txt-pageNumberOgranization').on('keydown', (e) => {
            if (!common.configs.keycodes.includes(e.keyCode))
                e.preventDefault();
        })

        $('.txt-pageNumberOgranization').on('keyup', (e) => {
            if (e.keyCode == common.configs.keys.Enter) {
                let isOk = true;

                if ($(e.target).val() == "" || $(e.target).val() == "0") {
                    $(e.target).val(1);
                    isOk = false;
                }

                if (isOk)
                    loadOrganization();
            }
            else
                this.page = parseInt(e.target.value);
        })

        $('#btnFirstPageOgranization').on('click', (e) => {
            if (this.page > 1) {
                this.page = 1;
                $('.txt-pageNumberOgranization').val(1);

                loadOrganization();
            }
        })

        $('#btnPrevPageOrganization').on('click', (e) => {
            if (this.page > 1) {
                this.page -= 1;
                $('.txt-pageNumberOgranization').val(this.page);

                loadOrganization();
            }
        })

        $('#btnLastPageOrganization').on('click', (e) => {
            this.page = this.totalPage;
            $('.txt-pageNumberOgranization').val(this.page);

            loadOrganization();
        })

        $('#btnNextPageOrganization').on('click', (e) => {
            if (this.page < this.totalPage) {
                this.page += 1;
                $('.txt-pageNumberOgranization').val(this.page);

                loadOrganization();
            }
        })

        $('.modal .form-control').on('click', (e) => {
            $(e.target).removeClass('error');
        })

        var organizationId;
        $("#table-oganization").on("click", "tbody tr", function () {
            organizationId = $(this).attr("data-id");
        });

        $(document).on('click', '.row-data .editRole', (e) => {
            e.preventDefault();
            openModal('#listRoleModal');
            loadOrganizationRole(organizationId);
        });

        $(document).on('click', '#changeRole', (e) => {
            let arrRole = getChangeRole();
            updateRoleOrganization(organizationId, arrRole)
        });
    }
    const getChangeRole = () => {
        let roles = $(document.querySelectorAll('#table-role tbody tr .cb-role'));
        let arrRoleChecked = [];
        roles.each((index, item) => {
            if (item.checked) {
                arrRoleChecked.push(item.value);
            }
        })

        return arrRoleChecked;
    }

    const openModal = (modal) => {
        $(modal).modal('show');
    }

    const closeModal = (modal) => {
        $(modal).modal('hide');
    }

    const generateTemplateOrganization = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data" data-id="' + item.id + '" > '
            + '<td><div class="text-left">' + item.orgName + '</div></td>'
            + '<td><div class="text-left">' + (item.shortDescription || "") + '</div></td>'
            + '<td><div class="text-left">' + (item.roleViewModels || "") + '</td>'
            + '<td><div class="text-left"><a class="editRole" href="#">Edit role</a></td>'
            + '</tr>';
    }

    const generateTemplateOrganizationRole = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data" data-id="' + item.id + '">'
            + '<td><div class="form-check"><input class="form-check-input cb-role" type="checkbox" value="' + item.id + '" id="role' + item.id + '"' + (item.isChecked ? "checked" : "") + '/>'
            + '<label class="form-check-label" for="role' + item.id + '" >' + item.name + '</label></div></td> '
            + '</tr>';
    }

    const generateTemplateOrganizationNoData = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
    }

    const loadOrganizationRole = (organizationId) => {
        const tbody = $('#table-role').find('tbody');
        $.ajax({
            method: "POST",
            url: "/Organization/GetRoleByOrganization",
            dataType: "json",
            data: {
                organizationId: organizationId,
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data.length > 0) {
                        res.data.forEach((item, i) => {
                            render += generateTemplateOrganizationRole(item);
                        })

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateTemplateOrganizationNoData);
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

    this.loadOrganization = (serviceId) => {
        const tbody = $('#table-oganization').find('tbody');
        servicesId = serviceId;
        $.ajax({
            method: "POST",
            url: "/Organization/GetOrganization",
            dataType: "json",
            data: {
                "page": this.page,
                "serviceId": serviceId,
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data.items.length > 0) {
                        res.data.items.forEach((item, i) => {
                            render += generateTemplateOrganization(item);
                        })

                        this.page = res.data.currentPage;
                        this.totalPage = res.data.pageCount;
                        $('.txt-pageNumber').val(this.page);
                        $('.totalPage').html(this.totalPage);

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateTemplateOrganizationNoData);
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

    const updateRoleOrganization = (organizationId, roleIds) => {
        $.ajax({
            method: "POST",
            url: "/Organization/UpdateRoleOrganization",
            dataType: "json",
            data: {
                organizationId: organizationId,
                roleIds: roleIds
            },
            success: (res) => {
                if (res.success) {
                    closeModal("#listRoleModal");
                    common.notify("Updated 1 record", "success");
                    this.loadOrganization(servicesId);
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }
}