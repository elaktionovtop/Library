using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class CorrectLoanRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanRecords_Books_BookId",
                table: "LoanRecords");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "LoanRecords",
                newName: "BookCopyId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRecords_BookId",
                table: "LoanRecords",
                newName: "IX_LoanRecords_BookCopyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "BookCopies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRecords_BookCopies_BookCopyId",
                table: "LoanRecords",
                column: "BookCopyId",
                principalTable: "BookCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanRecords_BookCopies_BookCopyId",
                table: "LoanRecords");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "BookCopies");

            migrationBuilder.RenameColumn(
                name: "BookCopyId",
                table: "LoanRecords",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRecords_BookCopyId",
                table: "LoanRecords",
                newName: "IX_LoanRecords_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRecords_Books_BookId",
                table: "LoanRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
