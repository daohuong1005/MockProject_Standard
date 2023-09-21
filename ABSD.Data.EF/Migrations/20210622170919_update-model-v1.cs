using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABSD.Data.EF.Migrations
{
    public partial class updatemodelv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Contents_Id",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Contracts_Id",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Participations_Id",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Contacts_ContactId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ContractContents_Id",
                table: "ContractContents");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContractContents");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceTimeLimited",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceStartExpected",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceStartDate",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceExtendable",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceEndDate",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractContents_ContractId",
                table: "ContractContents",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractContents_ParticipationId",
                table: "ContractContents",
                column: "ParticipationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Contents_ContentId",
                table: "ContractContents",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Contracts_ContractId",
                table: "ContractContents",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Participations_ParticipationId",
                table: "ContractContents",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Contacts_ContactId",
                table: "Services",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Contents_ContentId",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Contracts_ContractId",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractContents_Participations_ParticipationId",
                table: "ContractContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Contacts_ContactId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ContractContents_ContractId",
                table: "ContractContents");

            migrationBuilder.DropIndex(
                name: "IX_ContractContents_ParticipationId",
                table: "ContractContents");

            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceTimeLimited",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceStartExpected",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceStartDate",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceExtendable",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ServiceEndDate",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContractContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractContents_Id",
                table: "ContractContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Contents_Id",
                table: "ContractContents",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Contracts_Id",
                table: "ContractContents",
                column: "Id",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractContents_Participations_Id",
                table: "ContractContents",
                column: "Id",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Contacts_ContactId",
                table: "Services",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
