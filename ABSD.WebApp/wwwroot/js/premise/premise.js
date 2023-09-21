function premiseController() {
    this.page = 1;
    this.totalPage = 1;
    servicesId = serviceId;
    this.initializer = () => {
        registerEvents();
    }

    const registerEvents = () => {
        $('.modal .form-control').on('click', (e) => {
            $(e.target).removeClass('error');
        })

        $(document).on('click', '#premiseAssociate', (e) => {
            e.preventDefault();
            openModal('#listPremiseNoLinkModal');
            loadDataNotLinkPremise(servicesId);
        });

        $(document).on('change', '.checkBoxPremise', (e) => {
            if ($(e.target).is(':checked'))
                $(e.target).closest('td').find('.addLinkPremise').attr("disabled", false);
            else {
                $(e.target).closest('td').find('.addLinkPremise').val('').attr("disabled", true);
            }
        })

        $(document).on('click', '#btnAddPremise', (e) => {
            let premiseOject = getValuePrimes();
            updatePremiseLink(premiseOject);
            closeModal('#listPremiseNoLinkModal');
        })

        $(document).on('click', '.removePremise', (e) => {
            var presimeId = $(e.target).closest('tr').attr("data-id");
            removeServicePremise(presimeId, servicesId);
        })
    }
    const getValuePrimes = () => {
        let premise = $('.checkBoxPremise');
        let arrPremise = [];

        premise.each((index, item) => {
            if (item.checked) {
                arrPremise.push({
                    serviceId: servicesId,
                    premiseId: item.value,
                    projectCode: $(item).closest('td').find('.addLinkPremise').val()
                });
            }
        })

        return arrPremise;
    }

    const openModal = (modal) => {
        $(modal).modal('show');
    }

    const closeModal = (modal) => {
        $(modal).modal('hide');
    }

    const generateTemplatePremise = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data" data-id="' + item.id + '">'
            + '<td><div class="text-left">' + item.locationName + '</div></td>'
            + '<td><div class="text-left">' + item.addressLine + '</div></td>'
            + '<td><div class="text-left">' + item.phoneNumber + '</div></td>'
            + '<td><div class="text-left">' + (item.projectCode || "") + '</div></td>'
            + '<td><div class="text-left"><a href="#" class="removePremise">remove</a></td>'
            + '</tr>';
    }

    const generateTemplatePremiseProjectCode = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data" data-id="' + item.id + '">'
            + '<td><div class="text-left">' + item.locationName + '</div></td>'
            + '<td><div class="text-left">' + item.addressLine + '</div></td>'
            + '<td><div class="text-left">' + item.phoneNumber + '</div></td>'
            + '<td><div class="input-group mb-3"><div class="input-group-prepend"><div class="input-group-text">'
            + '<input type="checkbox" class="checkBoxPremise" value="' + item.id + '"></div></div>'
            + '<input type ="text" class="form-control addLinkPremise" disabled="true" value="' + (item.projectCode || "") + '"></div></td>'
            + '</tr>';
    }
    const generateTemplateNoData = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
    }

    this.loadDataPremise = (serviceId) => {
        const tbody = $('#table-Premise').find('tbody');
        servicesId = serviceId;
        $.ajax({
            method: "POST",
            url: "/Premise/Get",
            dataType: "json",
            data: {
                "serviceId": serviceId,
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data != null) {
                        res.data.forEach((item, i) => {
                            render += generateTemplatePremise(item);
                        })

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateTemplateNoData);
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

    const loadDataNotLinkPremise = (serviceId) => {
        const tbody = $('#table-listPremise').find('tbody');
        $.ajax({
            method: "POST",
            url: "/Premise/GetPremiseNotLink",
            dataType: "json",
            data: {
                serviceId: serviceId
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data != null) {
                        res.data.forEach((item, i) => {
                            render += generateTemplatePremiseProjectCode(item);
                        })

                        if (render.length > 0)
                            tbody.html(render);
                    }
                    else
                        tbody.html(generateTemplateNoData);
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

    const updatePremiseLink = (arrPremise) => {
        $.ajax({
            method: "POST",
            url: "/Premise/AssociateNewPremise",
            dataType: "json",
            data: {
                servicePremiseVM: arrPremise
            },
            success: (res) => {
                if (res.success) {
                    closeModal("#listRoleModal");
                    common.notify("Updated 1 record", "success");
                    this.loadDataPremise(servicesId);
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }
    const removeServicePremise = (premiseId, serviceId) => {
        $.ajax({
            method: "POST",
            url: "/Premise/RemoveServicePrimise",
            dataType: "json",
            data: {
                premiseId: premiseId,
                serviceId: serviceId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("Delete 1 record", "success");
                    this.loadDataPremise(servicesId);
                }
            },
            error: (res) => {
                common.notify('System error. Please try later', 'error');
            }
        });
    }
}