using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VngBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_PostCategories_ParentId",
                table: "PostCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCategories_CategoryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_PostCategories_ParentId",
                table: "PostCategories");

            migrationBuilder.DropIndex(
                name: "IX_PostCategories_Slug",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "PostCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PostCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "PostCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_PostId",
                table: "PostCategories",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_Categories_CategoryId",
                table: "PostCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_Posts_PostId",
                table: "PostCategories",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_Categories_CategoryId",
                table: "PostCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCategories_Posts_PostId",
                table: "PostCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories");

            migrationBuilder.DropIndex(
                name: "IX_PostCategories_PostId",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PostCategories");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PostCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTime",
                table: "PostCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PostCategories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "PostCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedTime",
                table: "PostCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PostCategories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "PostCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "PostCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_ParentId",
                table: "PostCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_Slug",
                table: "PostCategories",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCategories_PostCategories_ParentId",
                table: "PostCategories",
                column: "ParentId",
                principalTable: "PostCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCategories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
