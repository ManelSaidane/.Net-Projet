using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetRec.Migrations
{
    /// <inheritdoc />
    public partial class initialeeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACandidat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACandidat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEntreprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEntreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillsNeeded = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AJob_AEntreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "AEntreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AApplication_ACandidat_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "ACandidat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AApplication_AJob_JobId",
                        column: x => x.JobId,
                        principalTable: "AJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AApplication_CandidatId",
                table: "AApplication",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_AApplication_JobId",
                table: "AApplication",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AJob_EntrepriseId",
                table: "AJob",
                column: "EntrepriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AApplication");

            migrationBuilder.DropTable(
                name: "ACandidat");

            migrationBuilder.DropTable(
                name: "AJob");

            migrationBuilder.DropTable(
                name: "AEntreprise");
        }
    }
}
