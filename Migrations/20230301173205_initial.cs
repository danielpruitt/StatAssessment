using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatAssesment.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KomarExtract",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ClaimNumber = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    ClaimDate = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    OpenAmount = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    OriginalAmount = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    ARReasonCode = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CustomerReasonCode = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    AttachmentList = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CheckDate = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    DaysOutstanding = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    PONumber = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    MergeStatus = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    UnresolvedAmount = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    DocumentDate = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    OriginalCustomer = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CustomerLocation = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CreateDate = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    LoadId = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CarrierName = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    InvoiceStoreNumber = table.Column<string>(type: "nvarchar(2500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomarExtract", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomarExtract");
        }
    }
}
