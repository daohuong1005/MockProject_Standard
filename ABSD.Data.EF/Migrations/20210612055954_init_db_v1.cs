using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ABSD.Data.EF.Migrations
{
    public partial class init_db_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accreditations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accreditations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppClaims",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaims", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ClientSupports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSupports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Criterions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interventions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterventionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interventions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fundings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingSource = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    FundingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundingEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundraisingForText = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FundraisingWhy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FundraisingDonorAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    FundraisingDonorAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundingNeeds = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FundingContinuationNeeded = table.Column<bool>(type: "bit", nullable: false),
                    FundingContinuationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundingContinuationDetails = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FundraisingNeeded = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FundraisingRequiredBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundraisingComplete = table.Column<bool>(type: "bit", nullable: false),
                    FundraisingCompleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundraisingDonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundraisingDonationIncremental = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fundings_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleOrganizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOrganizations", x => new { x.OrganizationId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleOrganizations_AppRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ParticipationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractContents", x => new { x.ContentId, x.ContractId });
                    table.ForeignKey(
                        name: "FK_ContractContents_Contents_Id",
                        column: x => x.Id,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractContents_Contracts_Id",
                        column: x => x.Id,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractContents_Participations_Id",
                        column: x => x.Id,
                        principalTable: "Participations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ServiceShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ProgrammeId = table.Column<int>(type: "int", nullable: true),
                    ParticipationId = table.Column<int>(type: "int", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    FundingId = table.Column<int>(type: "int", nullable: true),
                    ClientDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ServiceStartExpected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceExtendable = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceTimeLimited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceFullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceActive = table.Column<bool>(type: "bit", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ServiceDesscriptionDelivery = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ServiceContactCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ServiceContractValue = table.Column<int>(type: "int", nullable: false),
                    ContractStagedPayment = table.Column<bool>(type: "bit", nullable: false),
                    ReferralProcess = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Fundings_FundingId",
                        column: x => x.FundingId,
                        principalTable: "Fundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Participations_ParticipationId",
                        column: x => x.ParticipationId,
                        principalTable: "Participations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TrustRegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_TrustRegionId",
                        column: x => x.TrustRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAccreditations",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    AccreditationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAccreditations", x => new { x.AccreditationId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceAccreditations_Accreditations_AccreditationId",
                        column: x => x.AccreditationId,
                        principalTable: "Accreditations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAccreditations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceClientSupports",
                columns: table => new
                {
                    ClientSupportId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceClientSupports", x => new { x.ServiceId, x.ClientSupportId });
                    table.ForeignKey(
                        name: "FK_ServiceClientSupports_ClientSupports_ClientSupportId",
                        column: x => x.ClientSupportId,
                        principalTable: "ClientSupports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceClientSupports_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCriterionSupports",
                columns: table => new
                {
                    CriterionId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCriterionSupports", x => new { x.ServiceId, x.CriterionId });
                    table.ForeignKey(
                        name: "FK_ServiceCriterionSupports_Criterions_CriterionId",
                        column: x => x.CriterionId,
                        principalTable: "Criterions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCriterionSupports_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInterventions",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInterventions", x => new { x.ServiceId, x.InterventionId });
                    table.ForeignKey(
                        name: "FK_ServiceInterventions_Interventions_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Interventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceInterventions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrganizations",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrganizations", x => new { x.OrganizationId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOrganizations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePremises",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    PremiseId = table.Column<int>(type: "int", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePremises", x => new { x.ServiceId, x.PremiseId });
                    table.ForeignKey(
                        name: "FK_ServicePremises_Premises_PremiseId",
                        column: x => x.PremiseId,
                        principalTable: "Premises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePremises_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractContents_Id",
                table: "ContractContents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_TrustRegionId",
                table: "Districts",
                column: "TrustRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundings_ContactId",
                table: "Fundings",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOrganizations_RoleId",
                table: "RoleOrganizations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccreditations_ServiceId",
                table: "ServiceAccreditations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceClientSupports_ClientSupportId",
                table: "ServiceClientSupports",
                column: "ClientSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCriterionSupports_CriterionId",
                table: "ServiceCriterionSupports",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInterventions_InterventionId",
                table: "ServiceInterventions",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrganizations_ServiceId",
                table: "ServiceOrganizations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePremises_PremiseId",
                table: "ServicePremises",
                column: "PremiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ContactId",
                table: "Services",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ContractId",
                table: "Services",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Services_FundingId",
                table: "Services",
                column: "FundingId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ParticipationId",
                table: "Services",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppClaims");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "ContractContents");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "RoleOrganizations");

            migrationBuilder.DropTable(
                name: "ServiceAccreditations");

            migrationBuilder.DropTable(
                name: "ServiceClientSupports");

            migrationBuilder.DropTable(
                name: "ServiceCriterionSupports");

            migrationBuilder.DropTable(
                name: "ServiceInterventions");

            migrationBuilder.DropTable(
                name: "ServiceOrganizations");

            migrationBuilder.DropTable(
                name: "ServicePremises");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "Accreditations");

            migrationBuilder.DropTable(
                name: "ClientSupports");

            migrationBuilder.DropTable(
                name: "Criterions");

            migrationBuilder.DropTable(
                name: "Interventions");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Fundings");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}