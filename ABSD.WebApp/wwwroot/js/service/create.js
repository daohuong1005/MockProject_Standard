function createServiceController() {
    this.initializer = () => {
        loadServiceType();
        loadParticipation();
       
        registerEvents();
    }
}

const registerEvents = (e) =>{
    $('#btnBackRegion').on('click', (e) => {
        window.location.href = "/service/Get";
    })
    $('#subTypeSelected').on('change', (e) => {
        let subType = $('#subTypeSelected').val();
		if (subType ==1) {
            $("#fundingtabs").removeClass('d-none');
        }
		if (subType==2) {
            $("#fundingtabs").addClass('d-none');
		}
    })

    $('#btnSaveService').on('click', (e) => {
        addService();
    })

    $('#btnBackService').on('click', (e) => {
        
        window.location.href = "/service/Index";
    })

    $(document).on('click', '#leadContact', (e) => {
        openContactModal('#contactModal');
        loadContact();
    })

    $('.btnSelectContact').on('click', (e) => {
        let contactName ;
        let contact = $('.radioContact');
        contact.each((index, item) => {
            if (item.checked) {
                contactName = $(item).closest('tr.row-data-contact').find('.contactName').html();
                console.log(contactName)
            }
        })

        $('#leadContact').val(contactName);
        closeContactModal('#contactModal');
    })

}

const openContactModal = (modal) => {
    $(modal).modal('show');
    
}
const closeContactModal = (modal) => {
    $(modal).modal('hide');

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

const loadParticipation = () => {
    const serviceAttendance = $('#serviceAttendance');
    $.ajax({
        method: "POST",
        url: "/Service/GetAllParticipations",
        dataType: "json",
        success: (res) => {

            if (res.success) {
                let render = '<option value = "" >--</option>';
                if (res.data.length > 0) {
                    res.data.forEach((item, i) => {
                        render += generateTemplateParticipation(item);
                    })

                    if (render.length > 0)
                        serviceAttendance.html(render);
                }
                else
                    serviceAttendance.html(generateTemplateNoData);
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

const generateTemplateNoData = () => {
    return '<tr class="row-data"><td colspan="5"><div class="">No Data</div></td></tr>';
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

const addService = () => {
    
    var fundingViewModel = {     
        FundingSource: $('#fundingSource').val(),
        ContactId: 1,
        FundingAmount: $('#fundingAmount').val(),
        FundingStart: $('#fundingStart').val(),
        FundingEnd: $('#fundingEnd').val(),
        FundraisingForText: $('#fundraisingForText').val(),
        FundraisingWhy: $('#fundraisingWhy').val(),
        FundraisingDonorAnonymous: $('#fundraisingDonorAnonymous').val(),
        FundraisingDonorAmount: $('#fundraisingDonorAmount').val(),
        FundingNeeds: $('#fundingNeed').val(),
        FundingContinuationNeeded: $('#continuationNeeded').val(),
        FundingContinuationAmount: $('#continuationAmount').val(),
        FundingContinuationDetails: $('#continuationDetails').val(),
        FundraisingNeeded: $('#fundraisingNeeds').val(),
        FundraisingRequiredBy: $('#fundraisingRequiredBy').val(),
        FundraisingComplete: $('#fundraisingComplete').val(),
        FundraisingCompleteDate: $('#fundraisingCompleteDate').val(),
        FundraisingDonationDate: $('#fundraisingDonationDate').val(),
        FundraisingDonationIncremental: $('#fundraisingDonationIncremental').val()
    };

    const serviceViewModel = {
        ServiceName:$('#serviceName').val(),
        ServiceShortDescription: $('#shortDescription').val(),
        FundingId: $('#subTypeSelected').val(),
        ContactId: 1,
        ServiceTypeId: $('#serviceType').val(),
        ParticipationId: $('#serviceAttendance').val(),
        ClientDescription: $('#clientDescription').val(),
        ServiceStartExpected: $('#startExpected').val(),
        ServiceStartDate: $('#startDate').val(),
        ServiceEndDate: $('#endDate').val(),
        ServiceExtendable: "2021-02-02",
        ServiceActive: $('#serviceActive').val(),
        ServiceFullDescription: $('#fullDescription').val(),
        DeptCode: $('#deptCode').val(),
        LeadContactId: 1,//$('#leadContact').val(),
        ServiceDesscriptionDelivery: $('#descriptionDelivery').val(),
        ServiceContactCode: $('#contractCode').val(),
        ServiceContractValue: $('#contractValue').val(),
        ContractStagedPayment: $('#contractStagedPayment').val(),
        ReferralProcess: $('#referralProcess').val(),
        ServiceTimeLimited: "2020-03-11",
        FundingViewModel: fundingViewModel
        //ServiceName: "Service test",
        //ServiceShortDescription: "Service test",
        //FundingId: 2,
        //ContactId: 2,
        //ServiceTypeId: 1,
        //ParticipationId: 1,
        //ClientDescription: "Service test",
        //ServiceStartExpected: "2021-02-02",
        //ServiceStartDate: "2021-02-02",
        //ServiceEndDate: "2021-02-02",
        //ServiceExtendable: "2021-02-02",
        //ServiceActive: true,
        //ServiceFullDescription: "Service test",
        //DeptCode: "Service test",
        //LeadContactId: 1,
        //ServiceDesscriptionDelivery: "Service test",
        //ServiceContactCode: "aaaaa",
        //ServiceContractValue: 1,
        //ContractStagedPayment: true,
        //ReferralProcess: "Service test",
        //ServiceTimeLimited: "2021-02-02",
       
    }

    if (serviceViewModel != null && fundingViewModel != null) {
        $.ajax({
            method: "POST",
            url: "/service/Create",
            dataType: "json",
            data: {
                "serviceViewModel": serviceViewModel         
            },
            success: (res) => {
                if (res.success) {
                    alert("Create successfully");
                }
                else {
                    common.notify(res.errorMessage, "error");
                }
            },
            error: (res) => {
                common.notify("System error. Please try later", "error");
                console.log(res);
            }
        });
    }
}