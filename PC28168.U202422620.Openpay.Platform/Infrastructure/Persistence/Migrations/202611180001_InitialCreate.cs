using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;
using PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence;

namespace PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Migrations;

[DbContext(typeof(SettlementsDbContext))]
[Migration("202611180001_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "disbursement_orders",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                order_tracking_code = table.Column<string>(type: "varchar(36)", nullable: false),
                affiliate_store_id = table.Column<string>(type: "varchar(36)", nullable: false),
                scheduled_from = table.Column<DateTime>(type: "date", nullable: false),
                scheduled_to = table.Column<DateTime>(type: "date", nullable: false),
                transfer_cap_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                transfer_cap_currency_code = table.Column<string>(type: "longtext", nullable: false),
                status = table.Column<int>(type: "int", nullable: false),
                user = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                remarks = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: true),
                created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_disbursement_orders", order => order.id);
            });

        migrationBuilder.CreateIndex(
            name: "ux_disbursement_orders_order_tracking_code",
            table: "disbursement_orders",
            column: "order_tracking_code",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "disbursement_orders");
    }
}
