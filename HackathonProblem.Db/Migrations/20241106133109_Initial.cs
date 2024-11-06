using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HackathonProblem.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hackathons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    harmonization = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hackathons", x => x.id);
                    table.CheckConstraint("Harmonization", "harmonization > 0");
                });

            migrationBuilder.CreateTable(
                name: "juniors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_juniors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "team_leads",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_leads", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "junior_preferences",
                columns: table => new
                {
                    hackathon_id = table.Column<int>(type: "integer", nullable: false),
                    junior_id = table.Column<int>(type: "integer", nullable: false),
                    desired_team_lead_id = table.Column<int>(type: "integer", nullable: false),
                    desired_team_lead_priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_junior_preferences", x => new { x.hackathon_id, x.junior_id, x.desired_team_lead_id, x.desired_team_lead_priority });
                    table.CheckConstraint("team-lead priority", "desired_team_lead_priority >= 0");
                    table.ForeignKey(
                        name: "fk_junior_preferences_hackathons_hackathon_id",
                        column: x => x.hackathon_id,
                        principalTable: "hackathons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_junior_preferences_juniors_junior_id",
                        column: x => x.junior_id,
                        principalTable: "juniors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_junior_preferences_team_leads_desired_team_lead_id",
                        column: x => x.desired_team_lead_id,
                        principalTable: "team_leads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_lead_preferences",
                columns: table => new
                {
                    hackathon_id = table.Column<int>(type: "integer", nullable: false),
                    team_lead_id = table.Column<int>(type: "integer", nullable: false),
                    desired_junior_id = table.Column<int>(type: "integer", nullable: false),
                    desired_junior_priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_lead_preferences", x => new { x.hackathon_id, x.team_lead_id, x.desired_junior_id, x.desired_junior_priority });
                    table.CheckConstraint("junior priority", "desired_junior_priority >= 0");
                    table.ForeignKey(
                        name: "fk_team_lead_preferences_hackathons_hackathon_id",
                        column: x => x.hackathon_id,
                        principalTable: "hackathons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_team_lead_preferences_juniors_desired_junior_id",
                        column: x => x.desired_junior_id,
                        principalTable: "juniors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_team_lead_preferences_team_leads_team_lead_id",
                        column: x => x.team_lead_id,
                        principalTable: "team_leads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    hackathon_id = table.Column<int>(type: "integer", nullable: false),
                    team_lead_id = table.Column<int>(type: "integer", nullable: false),
                    junior_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => new { x.hackathon_id, x.team_lead_id, x.junior_id });
                    table.ForeignKey(
                        name: "fk_teams_hackathons_hackathon_id",
                        column: x => x.hackathon_id,
                        principalTable: "hackathons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_juniors_junior_id",
                        column: x => x.junior_id,
                        principalTable: "juniors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_team_leads_team_lead_id",
                        column: x => x.team_lead_id,
                        principalTable: "team_leads",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_junior_preferences_desired_team_lead_id",
                table: "junior_preferences",
                column: "desired_team_lead_id");

            migrationBuilder.CreateIndex(
                name: "ix_junior_preferences_junior_id",
                table: "junior_preferences",
                column: "junior_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_lead_preferences_desired_junior_id",
                table: "team_lead_preferences",
                column: "desired_junior_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_lead_preferences_team_lead_id",
                table: "team_lead_preferences",
                column: "team_lead_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_junior_id",
                table: "teams",
                column: "junior_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_team_lead_id",
                table: "teams",
                column: "team_lead_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "junior_preferences");

            migrationBuilder.DropTable(
                name: "team_lead_preferences");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "hackathons");

            migrationBuilder.DropTable(
                name: "juniors");

            migrationBuilder.DropTable(
                name: "team_leads");
        }
    }
}
