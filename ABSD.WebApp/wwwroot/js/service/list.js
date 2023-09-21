function serviceController() {

    this.page = 1;
    this.totalPage = 1;
    this.initializer = () => {
        registerEvents();
        loadParticipation();
        loadContent();
        loadCriterion();
        loadClientSupport();
        loadServiceType();
        loadService();
    }

    const registerEvents = () => {
        $('#btnSave').on('click', (e) => {
            e.preventDefault();
            let isValid = true;
            //var serviceId = $('.radioService:checked').val();
            const serviceId = parseInt($('#AmedServiceModal #serviceId').val()) || 0;
            const serviceName = $('#AmedServiceModal #serviceName').val();
            const shortDescription = $('#AmedServiceModal #shortDescription').val();
            const serviceType = parseInt($('#AmedServiceModal #serviceType').val()) || 0;
            const serviceAttendance = parseInt($('#AmedServiceModal #serviceAttendance').val()) || 0;
            const serviceSubType = parseInt($('#AmedServiceModal #subTypeSelected').val()) || 0;

            if (!validateServiceName(serviceName))
                isValid = false;
            else
                hideServiceNameErrorMessage();

            if (!validateShortDescription(shortDescription))
                isValid = false;
            else
                hideShortDescriptionErrorMessage();

            if (!validateServiceType(serviceType))
                isValid = false;
            else
                hideServiceTypeErrorMessage();

            if (!validateServiceAttendance(serviceAttendance))
                isValid = false;
            else
                hideServiceAttendanceErrorMessage();

            if (!validateSubType(serviceSubType))
                isValid = false;
            else
                hideSubTypeErrorMessage();

            if (isValid) {
                if (serviceId === 0) {
                    addService(serviceSubType);
                    loadService();
                }
                else {
                    UpdateService(serviceId);
                    loadServiceType();
                    loadParticipation();
                    loadService();
                }
            }
        })

        $('#subTypeSelected').on('change', (e) => {
            let subType = $('#subTypeSelected').val();
            if (subType == 1) {
                $("#fundingtabs").removeClass('d-none');
                $("#contracttabs").addClass('d-none');
            }
            if (subType == 2) {
                $("#fundingtabs").addClass('d-none');
                $("#contracttabs").removeClass('d-none');
            }
            if (subType != 2 && subType != 1) {
                $("#fundingtabs").addClass('d-none');
                $("#contracttabs").addClass('d-none');
            }
        })

        $('#btnCreate').on('click', () => {
            refreshServiceModal()
            loadService();
            loadParticipation();
            openModal('#AmedServiceModal');
        })

        $('.btnCoppy').on('click', (e) => {
            let serviceId = $('.radioService:checked').val();
            if (serviceId > 0) {
                refreshServiceModal
                openModal('#AmedServiceModal');
                CopyService(serviceId);
                loadServiceType();
                loadParticipation();
            }
            else {
                common.notify("Please select a Service to copy")
            }
        })

        $(document).on('click', '#btnBack', () => {
            closeModal('#AmedServiceModal');
        });
        $('#subTypeSelected').on('change', (e) => {
            let subType = $('#subTypeSelected').val();
            if (subType == 1) {
                $("#fundingtabs").removeClass('d-none');
                $("#contracttabs").addClass('d-none');
            }
            if (subType == 2) {
                $("#fundingtabs").addClass('d-none');
                $("#contracttabs").removeClass('d-none');
            }
            if (subType != 2 && subType != 1) {
                $("#fundingtabs").addClass('d-none');
                $("#contracttabs").addClass('d-none');
            }
        })

        $('.label-filter').on('click', (e) => {
            const target = $(e.target);

            if (target.hasClass('active')) {
                target.removeClass('active');
                $('.label-filter').first().addClass('active');
            }
            else {
                $('.label-filter').removeClass('active');
                target.addClass('active');
            }

            loadService();
        });

        $('#cbIncludeInActive').on('change', () => {
            loadService();
        })

        $('.txt-pageNumber').on('keydown', (e) => {
            if (!common.configs.keycodes.includes(e.keyCode))
                e.preventDefault();
        })

        $('.txt-pageNumber').on('keyup', (e) => {
            if (e.keyCode == common.configs.keys.Enter) {
                let isOk = true;

                if ($(e.target).val() == "" || $(e.target).val() == "0") {
                    $(e.target).val(1);
                    isOk = false;
                }

                if (isOk)
                    loadService();
            }
            else
                this.page = parseInt(e.target.value);
        })

        $('#btnFirstPage').on('click', (e) => {
            if (this.page > 1) {
                this.page = 1;
                $('.txt-pageNumber').val(1);

                loadService();
            }
        })

        $('#btnPreviousPage').on('click', (e) => {
            if (this.page > 1) {
                this.page -= 1;
                $('.txt-pageNumber').val(this.page);

                loadService();
            }
        })

        $('#btnLastPage').on('click', (e) => {
            this.page = this.totalPage;
            $('.txt-pageNumber').val(this.page);

            loadService();
        })

        $('#btnNextPage').on('click', (e) => {
            if (this.page < this.totalPage) {
                this.page += 1;
                $('.txt-pageNumber').val(this.page);

                loadService();
            }
        })

        $(document).on('click', 'td.col-service-name .nav-link', (e) => {
            e.preventDefault();
            const row = $(e.target).closest('tr.row-data-service');
            const serviceId = parseInt(row.attr('data-id')) || 0;
            const isActive = row.attr('data-active') == "true" ? true : false;
            if (serviceId > 0 && isActive) {
                refreshServiceModal();
                openModal('#AmedServiceModal');
                organization.loadOrganization(serviceId);
                premise.loadDataPremise(serviceId);
                loadServiceType();
                loadParticipation();
                GetDetailService(serviceId);
            }
        })


        $(document).on('change', '#subTypeSelected', (e) => {
            let subType = $('#subTypeSelected').val();
            if (subType == 1) {
                $(".tab-funding").removeClass('d-none');
            }
            else if (subType == 2) {
                $(".tab-funding").addClass('d-none');
            }
            else if (subType == -1) {
                $(".tab-funding").addClass('d-none');
            }
        })

        $(document).on('click', '#leadContact', (e) => {
            openModal('#contactModal');
            loadContact();
        })

        $(document).on('click', '.btnSelectContact', (e) => {
            let contactName;
            let contact = $('.radioContact');
            contact.each((index, item) => {
                if (item.checked) {
                    contactName = $(item).closest('tr.row-data-contact').find('.contactName').html();
                    console.log(contactName)
                }
            })

            $('#leadContact').val(contactName);
            closeModal('#contactModal');
        })

        $(document).on('click', '#btnInActive', () => {
            const serviceId = parseInt($('input#serviceId').val());
            isActiveService(serviceId);
            closeModal('#AmedServiceModal');
        })


        $(document).on('dblclick', '.row-data-service', (e) => {
            e.preventDefault();

            const row = $(e.target).closest('.row-data-service');
            const id = parseInt(row.attr('data-id'));
            const isActive = row.attr('data-active') == "true" ? true : false;

            if (!isActive) {
                common.confirm("Do you want to make this Service active?", () => {
                    $.ajax({
                        url: "/service/active",
                        method: "POST",
                        dataType: "json",
                        data: {
                            serviceId: id
                        },
                        success: (res) => {
                            if (res.success) {
                                common.notify("successfully", "success")
                                loadService();
                            }
                            else
                                common.notify(res.errorMessage, "error");
                        },
                        error: (res) => {
                            common.notify("System error. Please try later", "error");
                        }
                    })
                });
            }
        })
    }

    const openModal = (modal) => {
        $(modal).modal('show');
    }

    const closeModal = (modal) => {
        $(modal).modal('hide');

        $('.tab-detail1').find('.nav-link').addClass('active');
        $('.tab-content #detail1').addClass('active');
        $('.tab-content #detail1').addClass('show');

        $('.tab-funding').find('.nav-link').removeClass('active');
        $('.tab-content #funding').removeClass('active');
        $('.tab-content #funding').removeClass('show');

        $('.tab-organization').find('.nav-link').removeClass('active');
        $('.tab-content #organization').removeClass('active');
        $('.tab-content #organization').removeClass('show');

        $('.tab-premise').find('.nav-link').removeClass('active');
        $('.tab-content #premise').removeClass('active');
        $('.tab-content #premise').removeClass('show');
    }

    const generatetemplate = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data-service" data-id="' + item.id + '" data-active="' + item.serviceactive + '">'
            + '<td><div><input type="radio" name="selected" class="text-left radioservice" id="' + item.id + '"></div></td>'
            + '<td class="col-service-name"><div class="text-left"><div class="text-left"><a class="nav-link p-0" href="/region/' + item.servicename + '">' + item.servicename + '</div></td>'
            + '<td><div class="text-left">' + item.serviceshortdescription + '</div></td>'
            + '<td><div class="text-left">' + item.servicetypeviewmodel.name + '</div></td>'
            + '<td><div class="text-left">' + item.contactviewmodel.firstname + ' ' + item.contactviewmodel.surname + '</div></td>'
            + '<td><div class="text-left">' + (item.serviceactive ? "yes" : "no") + '</div></td>'
            + '</tr>';
    }

    const generatetemplatenodata = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">no data</div></td></tr>';
    }

    const selectContact = (contactId) => {
        $.ajax({
            url: "/Service/GetContactById",
            method: "POST",
            dataType: "json",
            data: {
                contactId: contactId
            },
            success: (res) => {
                if (res.success) {

                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const loadContact = () => {

        const tbody = $('#table-contacts').find('tbody');

        $.ajax({
            method: "POST",
            url: "/Service/GetContacts",
            dataType: "json",
            data: {
                "page": this.page,
            },
            success: (res) => {
                if (res.success) {
                    let render = '';
                    if (res.data.items.length > 0) {
                        res.data.items.forEach((item, i) => {
                            render += generateTemplateContact(item);
                        })

                        this.page = res.data.currentPage;
                        this.totalPage = res.data.pageCount;
                        $('.txt-pageNumber').val(this.page);
                        $('.totalPage').html(this.totalPage);

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

    const loadServiceType = () => {
        const subType = $('#serviceType');
        $.ajax({
            method: "POST",
            url: "/Service/GetAllServiceTypes",
            dataType: "json",
            success: (res) => {

                if (res.success) {
                    let render = '<option value = "" >--</option>';
                    if (res.data.length > 0) {
                        res.data.forEach((item, i) => {
                            render += generateTemplateSubType(item);
                        })

                        if (render.length > 0)
                            subType.html(render);
                    }
                    else
                        subType.html(generateTemplateNoData);
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


    const generateTemplateSubType = (item) => {

        if (!item)
            return "";

        return '<option id="subType" value="' + item.id + '">' + item.name + '</option>'
    }

    const generateTemplateParticipation = (item) => {

        if (!item)
            return "";

        return '<option id="subTypeserviceAttendance" value="' + item.id + '">' + item.participationName + '</option>'
    }

    const generateTemplateContact = (item) => {

        if (!item)
            return "";

        return '<tr class="row-data-contact" data-id="' + item.id + '" data-active="' + item.isActive + '">'
            + '<td><div><input type="radio" name="selected" class="text-left radioContact" id="' + item.id + '"></div></td>'
            + '<td><div class="text-left contactName">' + item.firstName + ' ' + item.surName + '</div></td>'
            + '<td><div class="text-left">' + item.mobilePhone + '</div></td>'
            + '<td><div class="text-left">' + item.email + '</div></td>'
            + '<td><div class="text-left">' + item.contactType + '</div></td>'
            + '<td><div class="text-left">' + (item.isActive ? "Yes" : "No") + '</div></td>'
            + '</tr>';
    }

    const getServiceById = (servcieId) => {
        $.ajax({
            method: "POST",
            url: "/Service/GetService",
            dataType: "json",
            data:
            {
                serviceId: servcieId
            },
            success: (res) => {
                if (res.success) {
                    injectValue(res.data);
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

    const DeleteData = (serviceId, message) => {
        if (confirm(message)) {
            Delete(serviceId);
        }
        else {
            return false;
        }
    }

    const Delete = (serviceId) => {
        var url = '@Url.Content("~/")' + "Service/InActive/";
        $.post(url, { id: serviceId }, function (data) {
            if (data) {
                loadService();
            }
            else {
                common.notify("System error. Please try later", "error");
            }
        });
    }

    const loadParticipation = () => {
        $.ajax({
            method: "GET",
            url: "/Service/GetParticipation",
            dataType: "json",
            success: (res) => {
                if (res.success) {
                    let options = '<option value="">--</option>';
                    if (res.data.length > 0) {
                        res.data.forEach((item, i) => {
                            options += '<option value="' + item.id + '">' + item.participationName + '</option>'
                        })

                        if (options.length > 0) {
                            $('#slParticipation').html(options);
                            $('#serviceAttendance').html(options);
                        }

                    }
                }
            },
            error: (res) => {
                console.log(res);
            }
        });
    }

    const generateContentTemplate = (item) => {
        if (!item)
            return "";

        return '<input type="checkbox" class="contentCheckbox" value="' + item.id + '" id = "' + item.id + '">'
            + '<label for="' + item.id + '">' + item.contentName + '</label></br>';
    }

    const loadContent = () => {
        $.ajax({
            method: "GET",
            url: "/Service/GetContent",
            dataType: "json",
            success: (res) => {
                if (res.success) {
                    if (res.data.length > 0) {
                        var renderItem = '';
                        var renderTitle = '';
                        var typeName = res.data[0];
                        console.log(typeof (res.data));
                        var content = res.data[1];
                        typeName.forEach((item, i) => {
                            renderTitle += '<div class="col-md-6"><label class="font-weight-bold">' + item + '</label></br>';
                            content.forEach((contentItem, i) => {
                                if (contentItem.typeName == item)
                                    renderItem += generateContentTemplate(contentItem);
                            });
                            renderTitle += renderItem + '</div>';
                            if (renderTitle.length > 0) {
                                $('#cbContent').html(renderTitle);
                                renderItem = '';
                            }
                        });
                    }
                }
            },
            error: (res) => {
                console.log(res);
            }
        });
    }

    const generateCriterionTemplate = (item) => {
        if (!item)
            return "";

        return '<input type="checkbox" class="criterionCheckbox" value="' + item.id + '" id="' + item.id + '">'
            + '<label for="' + item.id + '">' + item.name + '</label></br>';
    }

    const loadCriterion = () => {
        $.ajax({
            method: "GET",
            url: "/Service/GetCriterion",
            dataType: "json",
            success: (res) => {
                if (res.success) {
                    if (res.data.length > 0) {
                        var renderItem = '';
                        var renderTitle = '';
                        var typeName = res.data[0];
                        var criterion = res.data[1];
                        typeName.forEach((item, i) => {
                            renderTitle += '<div class="col-md-6"><label class="font-weight-bold">' + item + '</label></br>';
                            criterion.forEach((criterionItem, i) => {
                                if (criterionItem.typeName == item)
                                    renderItem += generateCriterionTemplate(criterionItem);
                            });
                            renderTitle += renderItem + '</div>';
                            if (renderTitle.length > 0) {
                                $('#cbCriterion').html(renderTitle);
                                renderItem = '';
                            }
                        });
                    }
                }
            }
        });
    }

    const generateClientSupportTemplate = (item) => {
        if (!item)
            return "";

        return '<input type="checkbox" class="clientSupportCheckbox" value="' + item.id + '" id="' + item.id + '">'
            + '<label for="' + item.id + '">' + item.name + '</label></br>';
    }

    const loadClientSupport = () => {
        $.ajax({
            method: "GET",
            url: "/Service/GetClientSupport",
            dataType: "json",
            success: (res) => {
                if (res.success) {
                    if (res.data.length > 0) {
                        var renderItem = '';
                        var renderTitle = '';
                        var typeName = res.data[0];
                        var clientSupport = res.data[1];
                        typeName.forEach((item, i) => {
                            renderTitle += '<div class="col-md-6"><label class="font-weight-bold">' + item + '</label></br>';
                            clientSupport.forEach((clientSupportItem, i) => {
                                if (clientSupportItem.typeName == item)
                                    renderItem += generateClientSupportTemplate(clientSupportItem);
                            });
                            renderTitle += renderItem + '</div>';
                            if (renderTitle.length > 0) {
                                $('#cbClientSupport').html(renderTitle);
                                renderItem = '';
                            }
                        });
                    }
                }
            }
        });
    }

    const generateTemplate = (item) => {
        if (!item)
            return "";

        return '<tr class="row-data-service" data-id="' + item.id + '" data-active="' + item.serviceActive + '">'
            + '<td><div><input type="radio" name="selected" class="text-left radioService" value="' + item.id + '" id="' + item.id + '"></div></td>'
            + '<td class="col-service-name"><div class="text-left"><div class="text-left"><a class="nav-link p-0" href="/region/' + item.serviceName + '">' + item.serviceName + '</div></td>'
            + '<td><div class="text-left row-data">' + item.serviceShortDescription + '</div></td>'
            + '<td><div class="text-left row-data">' + item.serviceTypeViewModel.name + '</div></td>'
            + '<td><div class="text-left row-data">' + item.contactViewModel.firstName + ' ' + item.contactViewModel.surName + '</div></td>'
            + '<td><div class="text-left row-data">' + (item.serviceActive ? "Yes" : "No") + '</div></td>'
            + '</tr>';
    }

    const generateTemplateNoData = () => {
        return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
    }


    const loadService = () => {
        const firstCharacters = common.getFilterName(parseInt($('.label-filter.active').attr('data-type')));
        const includeInActive = $('#cbIncludeInActive').is(":checked");
        const tbody = $('#table-services').find('tbody');

        $.ajax({
            method: "POST",
            url: "/Service/Get",
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
                            render += generateTemplate(item);
                        })

                        this.page = res.data.currentPage;
                        this.totalPage = res.data.pageCount;
                        $('.txt-pageNumber').val(this.page);
                        $('.totalPage').html(this.totalPage);

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

    const addService = (subTypeValue) => {

        var fundingViewModel;
        var participationViewModel = {
            Id: $('#slParticipation').val(), ParticipatinName: $('#slParticipation').text()
        };

        var criterionViewModel = [];
        $('.criterionCheckbox:checked').each(function (i) {
            criterionViewModel[i] = { Id: $(this).val() };
        });

        var clientSupportViewModel = [];
        $('.clientSupportCheckbox:checked').each(function (i) {
            clientSupportViewModel[i] = { Id: $(this).val() };
        })

        if (subTypeValue == 1) {
            fundingViewModel = {
                FundingSource: $('#fundingSource').val(),
                ContactId: 1,
                FundingAmount: $('#fundingAmount').val(),
                FundingStart: $('#fundingStart').val(),
                FundingEnd: $('#fundingEnd').val(),
                FundraisingForText: $('#fundraisingForText').val(),
                FundraisingWhy: $('#fundraisingWhy').val(),
                FundraisingDonorAnonymous: $('#fundraisingDonorAnonymous').is(":checked") ? true : false,
                FundraisingDonorAmount: $('#fundraisingDonorAmount').val(),
                FundingNeeds: $('#fundingNeed').val(),
                FundingContinuationNeeded: $('#continuationNeeded').is(":checked") ? true : false,
                FundingContinuationAmount: $('#continuationAmount').val(),
                FundingContinuationDetails: $('#continuationDetails').val(),
                FundraisingNeeded: $('#fundraisingNeeds').is(":checked") ? true : false,
                FundraisingRequiredBy: $('#fundraisingRequiredBy').val(),
                FundraisingComplete: $('#fundraisingComplete').is(":checked") ? true : false,
                FundraisingCompleteDate: $('#fundraisingCompleteDate').val(),
                FundraisingDonationDate: $('#fundraisingDonationDate').val(),
                FundraisingDonationIncremental: $('#fundraisingDonationIncremental').is(":checked") ? true : false
            };
        }
        else if (subTypeValue == 2) {
            var contentViewModel = [];
            $('.contentCheckbox:checked').each(function (i) {
                contentViewModel[i] = { Id: $(this).val() };
            });
            fundingViewModel = null;
        }

        const serviceViewModel = {
            ServiceName: $('#serviceName').val(),
            ServiceShortDescription: $('#shortDescription').val(),
            FundingId: $('#fundingId').val(),
            ContactId: 1,
            ServiceTypeId: $('#serviceType').val(),
            ParticipationId: $('#serviceAttendance').val(),
            ClientDescription: $('#clientDescription').val(),
            ServiceStartExpected: $('#startExpected').val(),
            ServiceStartDate: $('#startDate').val(),
            ServiceEndDate: $('#endDate').val(),
            ServiceExtendable: $('#AmedServiceModal #entendableYear').val() + '-' + $('#AmedServiceModal #entendableMonth').val() + '-' + 1,
            ServiceActive: $('#serviceActive').is(":checked") ? true : false,
            ServiceFullDescription: $('#fullDescription').val(),
            DeptCode: $('#deptCode').val(),
            LeadContactId: 1,
            ServiceDesscriptionDelivery: $('#descriptionDelivery').val(),
            ServiceContactCode: $('#contractCode').val(),
            ServiceContractValue: $('#contractValue').val(),
            ContractStagedPayment: $('#contractStagedPayment').is(":checked") ? true : false,
            ReferralProcess: $('#referralProcess').val(),
            ServiceTimeLimited: $('#AmedServiceModal #limitedYear').val() + '-' + $('#AmedServiceModal #limitedMonth').val() + '-' + 1,
            FundingViewModel: fundingViewModel,
            CriterionViewModel: criterionViewModel,
            ParticipationViewModel: participationViewModel,
            ClientSupportViewModels: clientSupportViewModel,
            ContentViewModels: contentViewModel
        }

        if (serviceViewModel != null) {
            $.ajax({
                method: "POST",
                url: "/Service/Create",
                dataType: "json",
                data: {
                    "serviceViewModel": serviceViewModel
                },
                success: (res) => {
                    if (res.success) {
                        closeModal('#AmedServiceModal');
                        common.notify("Inserted 1 record", "success");
                        loadService();
                    }
                    else {
                        if (res.code == -1) {
                            common.notify(res.errorMessage, "error");
                        }
                        else {
                            showServiceNameError(res.errorMessage);
                        }
                    }
                },
                error: (res) => {
                    common.notify("System error. Please try later", "error");
                    console.log(res);
                }
            });
        }
    }

    const UpdateService = (itemId) => {
        var contentViewModel = [];
        $('.contentCheckbox:checked').each(function (i) {
            contentViewModel[i] = { Id: $(this).val() };
        });

        var participationViewModel = {
            Id: $('#slParticipation').val(), ParticipatinName: $('#slParticipation').text()
        };

        var criterionViewModel = [];
        $('.criterionCheckbox:checked').each(function (i) {
            criterionViewModel[i] = { Id: $(this).val() };
        });

        var clientSupportViewModel = [];
        $('.clientSupportCheckbox:checked').each(function (i) {
            clientSupportViewModel[i] = { Id: $(this).val() };
        })

        var fundingViewModel = {
            FundingSource: $('#fundingSource').val(),
            ContactId: 1,
            FundingAmount: $('#fundingAmount').val(),
            FundingStart: $('#fundingStart').val(),
            FundingEnd: $('#fundingEnd').val(),
            FundraisingForText: $('#fundraisingForText').val(),
            FundraisingWhy: $('#fundraisingWhy').val(),
            FundraisingDonorAnonymous: $('#fundraisingDonorAnonymous').is(":checked") ? true : false,
            FundraisingDonorAmount: $('#fundraisingDonorAmount').val(),
            FundingNeeds: $('#fundingNeed').val(),
            FundingContinuationNeeded: $('#continuationNeeded').is(":checked") ? true : false,
            FundingContinuationAmount: $('#continuationAmount').val(),
            FundingContinuationDetails: $('#continuationDetails').val(),
            FundraisingNeeded: $('#fundraisingNeeds').is(":checked") ? true : false,
            FundraisingRequiredBy: $('#fundraisingRequiredBy').val(),
            FundraisingComplete: $('#fundraisingComplete').is(":checked") ? true : false,
            FundraisingCompleteDate: $('#fundraisingCompleteDate').val(),
            FundraisingDonationDate: $('#fundraisingDonationDate').val(),
            FundraisingDonationIncremental: $('#fundraisingDonationIncremental').is(":checked") ? true : false
        }

        const serviceViewModel = {
            Id: $('#serviceId').val(),
            ServiceName: $('#serviceName').val(),
            ServiceShortDescription: $('#shortDescription').val(),
            FundingId: $('#fundingId').val(),
            ContactId: 1,
            ServiceTypeId: $('#serviceType').val(),
            ParticipationId: $('#serviceAttendance').val(),
            ClientDescription: $('#clientDescription').val(),
            ServiceStartExpected: $('#startExpected').val(),
            ServiceStartDate: $('#startDate').val(),
            ServiceEndDate: $('#endDate').val(),
            ServiceExtendable: $('#AmedServiceModal #entendableYear').val() + '-' + $('#AmedServiceModal #entendableMonth').val() + '-' + 1,
            ServiceActive: $('#serviceActive').is(":checked") ? true : false,
            ServiceFullDescription: $('#fullDescription').val(),
            DeptCode: $('#deptCode').val(),
            LeadContactId: 1,
            ServiceDesscriptionDelivery: $('#descriptionDelivery').val(),
            ServiceContactCode: $('#contractCode').val(),
            ServiceContractValue: $('#contractValue').val(),
            ContractStagedPayment: $('#contractStagedPayment').is(":checked") ? true : false,
            ReferralProcess: $('#referralProcess').val(),
            ServiceTimeLimited: $('#AmedServiceModal #limitedYear').val() + '-' + $('#AmedServiceModal #limitedMonth').val() + '-' + 1,
            FundingViewModel: fundingViewModel,
            CriterionViewModel: criterionViewModel,
            ParticipationViewModel: participationViewModel,
            ClientSupportViewModels: clientSupportViewModel,
            ContentViewModels: contentViewModel
        }

        if (serviceViewModel != null) {
            $.ajax({
                method: "POST",
                url: "/Service/Update",
                dataType: "json",
                data: {
                    "serviceViewModel": serviceViewModel
                },
                success: (res) => {
                    if (res.success) {
                        closeModal('#AmedServiceModal');
                        common.notify("Updated successfully", "success");
                        loadService();
                    }
                    else {
                        if (res.code == -1) {
                            common.notify(res.errorMessage, "error");
                        }
                        else {
                            showServiceNameError(res.errorMessage);
                        }
                    }
                },
                error: (res) => {
                    common.notify("System error. Please try later", "error");
                    console.log(res);
                }
            });
        }
    }

    const refreshServiceModal = () => {
        $('.contentCheckbox:checked').each(function (i) {
            $(this).prop('checked', false);
        });

        $('#slParticipation').val('');

        $('.criterionCheckbox').each(function (i) {
            $(this).prop('checked', false);
        });

        $('.clientSupportCheckbox').each(function (i) {
            $(this).prop('checked', false);
        })

        $('#serviceName').val('');
        $('#shortDescription').val('');
        $('#fullDescription').val('');
        $("#serviceActive").prop('checked', true);
        $('#serviceType').val('');
        $('#serviceAttendance').val('');
        $('#deptCode').val('');
        $('#leadContact').val('');
        $('#clientDescription').val('');
        $('#descriptionDelivery').val('');
        $('#contractCode').val('');
        $('#startExpected').val('');
        $('#contractValue').val('');
        $('#startDate').val('');
        $("#contractStagedPayment").prop('checked', false);
        $('#endDate').val('');
        $('#referralProcess').val('');
        $('#AmedServiceModal #limitedYear').val("");
        $('#AmedServiceModal #limitedMonth').val("");



        $('#fundingSource').val('');
        $('#fundingAmount').val('');
        $('#fundingStart').val('');
        $('#fundingEnd').val('');
        $('#fundraisingForText').val('')
        $('#fundraisingWhy').val('');
        $("#fundraisingDonorAnonymous").val('');
        $('#fundraisingDonorAmount').val('');
        $('#fundingNeed').val('');
        $('#continuationNeeded').val('');
        $('#continuationAmount').val('');
        $('#continuationDetails').val('');
        $("#fundraisingNeeds").prop('checked', false);
        $('#fundraisingRequiredBy').val('');
        $('#fundraisingComplete').val('');
        $('#fundraisingCompleteDate').val('');
        $('#fundraisingDonationDate').val('');
        $("#fundraisingDonationIncremental").val('');

    }

    const CopyService = () => {
        var serviceId = $('.radioService:checked').val();
        var fundingViewModel;
        var criterionList;
        var clientSupportList;
        var contentList;
        $.ajax({
            method: "POST",
            url: '/Service/Copy',
            dataType: "json",
            data: { "serviceId": serviceId },
            success: (res) => {
                if (res.success) {
                    ShowServiceInforCopy(res.data);
                    fundingViewModel = res.data.fundingViewModel;
                    ShowFundingInfo(fundingViewModel);
                    criterionList = res.data.criterionViewModel;
                    clientSupportList = res.data.clientSupportViewModels;
                    contentList = res.data.contentViewModels;
                    MakeCheckboxesChecked(criterionList, '.criterionCheckbox');
                    MakeCheckboxesChecked(clientSupportList, '.clientSupportCheckbox');
                    MakeCheckboxesChecked(contentList, '.contentCheckbox');
                    $('#slParticipation').val(res.data.participationId);
                }
            }
        });

    }

    const GetDetailService = (serviceId) => {

        var fundingViewModel;
        var criterionList;
        var clientSupportList;
        var contentList;
        $.ajax({
            method: "POST",
            url: '/Service/GetDetailService',
            dataType: "json",
            data: { "serviceId": serviceId },
            success: (res) => {
                if (res.success) {
                    ShowServiceInfor(res.data);
                    fundingViewModel = res.data.fundingViewModel;
                    if (fundingViewModel != null)
                        ShowFundingInfo(fundingViewModel);
                    criterionList = res.data.criterionViewModel;
                    clientSupportList = res.data.clientSupportViewModels;
                    contentList = res.data.contentViewModels;
                    MakeCheckboxesChecked(criterionList, '.criterionCheckbox');
                    MakeCheckboxesChecked(clientSupportList, '.clientSupportCheckbox');
                    MakeCheckboxesChecked(contentList, '.contentCheckbox');
                    console.log(res.data);
                    $('#slParticipation').val(res.data.participationViewModel.id);
                }
            }
        });
    }

    const MakeCheckboxesChecked = (checkedItemList, elements) => {
        checkedItemList.forEach((item, i) => {
            $(elements).each(function (i) {
                if ($(this).val() == item.id) {
                    $(this).prop('checked', true);
                }
            })
        })
    }

    const ShowServiceInforCopy = (item) => {
        const entendable = new Date(item.serviceExtendable);
        const limited = new Date(item.serviceTimeLimited);

        $('#serviceId').val("");
        $('#serviceName').val("");
        $('#shortDescription').val(item.serviceShortDescription);
        $('#fullDescription').val(item.serviceFullDescription);
        if (item.serviceActive) {
            $("#serviceActive").prop('checked', true);
        }
        $('#serviceType').val(item.serviceTypeId);
        $('#serviceAttendance').val(item.participationId);
        $('#deptCode').val(item.deptCode);
        $('#leadContact').val(item.contactId);
        $('#clientDescription').val(item.clientDescription);
        $('#descriptionDelivery').val(item.serviceDesscriptionDelivery);
        $('#contractCode').val(item.serviceContactCode);
        $('#startExpected').val(dateFormatJson(item.serviceStartExpected));
        $('#contractValue').val(item.serviceContractValue);
        $('#startDate').val(dateFormatJson(item.serviceStartDate));
        $('#entendableYear').val(entendable.getFullYear());
        $('#entendableMonth').val(entendable.getMonth() + 1);

        $('#limitedYear').val(limited.getFullYear());
        $('#limitedMonth').val(limited.getMonth() + 1);

        if (item.contractStagedPayment) {
            $("#contractStagedPayment").prop('checked', true);
        }

        $('#endDate').val(dateFormatJson(item.serviceEndDate));
        $('#referralProcess').val(item.referralProcess);

    }

    const ShowFundingInfo = (item) => {

        $('#fundingSource').val(item.fundingSource);
        $('#fundingAmount').val(item.fundingAmount);
        $('#fundingStart').val(item.fundingStart);
        $('#fundingEnd').val(item.fundingEnd);
        $('#fundraisingForText').val(item.fundraisingForText)
        $('#fundraisingWhy').val(item.fundraisingWhy);

        if (item.fundraisingDonorAnonymous) {
            $("#fundraisingDonorAnonymous").prop('checked', true);
        }

        $('#fundraisingDonorAmount').val(item.fundraisingDonorAmount);
        $('#fundingNeed').val(item.fundingNeeds);

        if (item.fundingContinuationNeeded) {
            $("#continuationNeeded").prop('checked', true);
        }

        $('#continuationAmount').val(item.fundingContinuationAmount);
        $('#continuationDetails').val(item.fundingContinuationDetails);

        if (item.fundraisingNeeded) {
            $("#fundraisingNeeds").prop('checked', true);
        }

        $('#fundraisingRequiredBy').val(dateFormatJson(item.fundraisingRequiredBy));
        if (item.fundraisingComplete) {
            $("#fundraisingComplete").prop('checked', true);
        }

        $('#fundraisingCompleteDate').val(dateFormatJson(item.fundraisingCompleteDate));
        $('#fundraisingDonationDate').val(dateFormatJson(item.fundraisingDonationDate));

        if (item.fundraisingDonationIncremental) {
            $("#fundraisingDonationIncremental").prop('checked', true);
        }

    }

    const ShowServiceInfor = (item) => {
        const entendable = new Date(item.serviceExtendable);
        const limited = new Date(item.serviceTimeLimited);

        $('#serviceId').val(item.id);
        $('#fundingId').val(item.fundingId);
        $('#serviceName').val(item.serviceName);
        $('#shortDescription').val(item.serviceShortDescription);
        $('#fullDescription').val(item.serviceFullDescription);
        if (item.serviceActive) {
            $("#serviceActive").prop('checked', true);
        }
        $('#serviceType').val(item.serviceTypeId);
        $('#serviceAttendance').val(item.participationId);
        $('#deptCode').val(item.deptCode);
        $('#leadContact').val(item.contactId);
        $('#clientDescription').val(item.clientDescription);
        $('#descriptionDelivery').val(item.serviceDesscriptionDelivery);
        $('#contractCode').val(item.serviceContactCode);
        $('#startExpected').val(dateFormatJson(item.serviceStartExpected));
        $('#contractValue').val(item.serviceContractValue);
        $('#startDate').val(dateFormatJson(item.serviceStartDate));
        $('#entendableYear').val(entendable.getFullYear());
        $('#entendableMonth').val(entendable.getMonth() + 1);

        $('#limitedYear').val(limited.getFullYear());
        $('#limitedMonth').val(limited.getMonth() + 1);

        if (item.contractStagedPayment) {
            $("#contractStagedPayment").prop('checked', true);
        }

        $('#endDate').val(dateFormatJson(item.serviceEndDate));
        $('#referralProcess').val(item.referralProcess);

    }
    const dateFormatJson = (date) => {
        if (!date)
            return '';

        const newDate = new Date(date);

        let month = newDate.getMonth() + 1;
        let day = newDate.getDate();
        let year = newDate.getFullYear();

        if (month < 10) month = "0" + month;
        if (day < 10) day = "0" + day;

        return year + "-" + month + "-" + day;
    }
    const showServiceTypeError = (message) => {
        $('#serviceType').addClass('error');
        $('.modal .serviceType-error').html(message);
    }

    const hideServiceTypeErrorMessage = (message) => {
        $('.modal .serviceType-error').html('');
    }

    const showSubTypeError = (message) => {
        $('#subTypeSelected').addClass('error');
        $('.modal .subType-error').html(message);
    }

    const hideSubTypeErrorMessage = (message) => {
        $('.modal .subType-error').html('');
    }

    const showServiceAttendanceError = (message) => {
        $('#serviceAttendance').addClass('error');
        $('.modal .serviceAttendance-error').html(message);
    }

    const hideServiceAttendanceErrorMessage = () => {
        $('.modal .serviceAttendance-error').html('');
    }

    const showServiceNameError = (message) => {
        $('#serviceName').addClass('error');
        $('.modal .servicename-error').html(message);
    }

    const hideServiceNameErrorMessage = () => {
        $('.modal .servicename-error').html('');
    }

    const showShortDescriptionError = (message) => {
        $('#shortDescription').addClass('error');
        $('.modal .shortDescription-error').html(message);
    }

    const hideShortDescriptionErrorMessage = () => {
        $('.modal .shortDescription-error').html('');
    }

    const validateServiceType = (value) => {
        if (value == 0) {
            showServiceTypeError('Please select the Service Type');
            return false;
        }

        return true;
    }

    const validateSubType = (value) => {
        if (value == -1) {
            showSubTypeError('Please select the Sub Type');
            return false;
        }

        return true;
    }

    const validateServiceAttendance = (value) => {
        if (value == 0) {
            showServiceAttendanceError('Please select the Service Attendance');
            return false;
        }

        return true;
    }

    const validateServiceName = (value) => {
        if (value.trim().length == 0) {
            showServiceNameError('Please input the Service Name');
            return false;
        }
        return true;
    }

    const validateShortDescription = (value) => {
        if (value.trim().length == 0) {
            showShortDescriptionError('Please input the Short Description');
            return false;
        }

        return true;
    }

    const isActiveService = (serviceId) => {
        $.ajax({
            url: "/Service/IsSeviceLink",
            method: "POST",
            dataType: "json",
            data: {
                serviceId: serviceId
            },
            success: (res) => {
                if (res.success) {
                    //openModal("#confirmInActive");
                    if (res.data == 1) {
                        const messageConfirm = "This Service is already in use, do you want to make this in-active ?";
                        common.confirm(messageConfirm, () => {
                            InAciveService(serviceId)
                        });
                    }
                    else if (res.data == 0) {
                        const messageConfirm = "Do you want to mark this Service in-active?";
                        common.confirm(messageConfirm, () => {
                            InAciveService(serviceId)
                        });
                    }
                }
                else
                    common.notify(res.errorMessage, "error");
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
            }
        })
    }

    const InAciveService = (serviceId) => {
        $.ajax({
            url: "/Service/InActive",
            method: "POST",
            dataType: "json",
            data: {
                ServiceId: serviceId
            },
            success: (res) => {
                if (res.success) {
                    common.notify("successfully", "success")
                    loadService();
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