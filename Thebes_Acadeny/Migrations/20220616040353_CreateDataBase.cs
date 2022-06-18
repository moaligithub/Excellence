using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thebes_Acadeny.Migrations
{
    public partial class CreateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categores",
                columns: table => new
                {
                    GategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categores", x => x.GategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "MessagesUsers",
                columns: table => new
                {
                    MessageUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bol = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesUsers", x => x.MessageUserId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtiesName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.SpecialtiesId);
                });

            migrationBuilder.CreateTable(
                name: "Tearms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tearms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlWebSite",
                columns: table => new
                {
                    UrlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlWebSite", x => x.UrlId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumper = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelIdFk = table.Column<int>(type: "int", nullable: false),
                    GategoryIdFk = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Categores_GategoryIdFk",
                        column: x => x.GategoryIdFk,
                        principalTable: "Categores",
                        principalColumn: "GategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admins_Levels_LevelIdFk",
                        column: x => x.LevelIdFk,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admins_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtiesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelIdFk = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesIdFk = table.Column<int>(type: "int", nullable: false),
                    TearmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Plants_Levels_LevelIdFk",
                        column: x => x.LevelIdFk,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Specialties_SpecialtiesIdFk",
                        column: x => x.SpecialtiesIdFk,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtiesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Tearms_TearmId",
                        column: x => x.TearmId,
                        principalTable: "Tearms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BooksAdmins",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assent = table.Column<bool>(type: "bit", nullable: false),
                    AdminIdFk = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAdmins", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BooksAdmins_Admins_AdminIdFk",
                        column: x => x.AdminIdFk,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksAdmins_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamAdmins",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assent = table.Column<bool>(type: "bit", nullable: false),
                    AdminIdFk = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAdmins", x => x.ExamId);
                    table.ForeignKey(
                        name: "FK_ExamAdmins_Admins_AdminIdFk",
                        column: x => x.AdminIdFk,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAdmins_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostsAdmins",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assent = table.Column<bool>(type: "bit", nullable: false),
                    AdminIdFk = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsAdmins", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostsAdmins_Admins_AdminIdFk",
                        column: x => x.AdminIdFk,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsAdmins_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoAdmins",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assent = table.Column<bool>(type: "bit", nullable: false),
                    AdminIdFk = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAdmins", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_VideoAdmins_Admins_AdminIdFk",
                        column: x => x.AdminIdFk,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoAdmins_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question_True_or_false",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<bool>(type: "bit", nullable: false),
                    ExamIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_True_or_false", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_True_or_false_ExamAdmins_ExamIdFk",
                        column: x => x.ExamIdFk,
                        principalTable: "ExamAdmins",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_ExamAdmins_ExamIdFk",
                        column: x => x.ExamIdFk,
                        principalTable: "ExamAdmins",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    boolAnswer = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answer_ExamAdmins_ExamId",
                        column: x => x.ExamId,
                        principalTable: "ExamAdmins",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categores",
                columns: new[] { "GategoryId", "CategoryName" },
                values: new object[,]
                {
                    { -1, "اختر الفئه" },
                    { 1, "Owner" },
                    { 2, "AdminShimaa" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelId", "LevelName" },
                values: new object[,]
                {
                    { -1, "اختر الفرقه" },
                    { 1, "الفرقه الاولي" },
                    { 2, "الفرقه الثانيه" },
                    { 3, "الفرقه الثالثه" },
                    { 4, "الفرقه الرابعه" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtiesId", "SpecialtiesName" },
                values: new object[,]
                {
                    { -1, "اختر التخصص" },
                    { 1, "نظم المعلومات الاداريه" }
                });

            migrationBuilder.InsertData(
                table: "Tearms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "الترم الاول" },
                    { 2, "الترم الثاني" }
                });

            migrationBuilder.InsertData(
                table: "UrlWebSite",
                columns: new[] { "UrlId", "UrlText" },
                values: new object[] { 1, "https://localhost:44347/" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "FullName", "GategoryIdFk", "ImageUrl", "LevelIdFk", "Password", "PhoneNumper", "SpecialtiesId", "UserName", "WhatsApp" },
                values: new object[] { -92539587, "Mohamed Ali", 1, "InShot_20211111_204615744.jpg", 2, "nd112004", "01159252091", 1, "mo112004", "Https://api.whatsapp.com/send?phone=0201159252091" });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "PlantId", "LevelIdFk", "PlantName", "SpecialtiesIdFk", "TearmId" },
                values: new object[] { -1, 1, "اختر ماده", 1, null });

            migrationBuilder.InsertData(
                table: "ExamAdmins",
                columns: new[] { "ExamId", "AdminIdFk", "Assent", "PlantId", "Title" },
                values: new object[] { -1, -92539587, true, null, "اختر امتحان" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_GategoryIdFk",
                table: "Admins",
                column: "GategoryIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_LevelIdFk",
                table: "Admins",
                column: "LevelIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SpecialtiesId",
                table: "Admins",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_ExamId",
                table: "Answer",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAdmins_AdminIdFk",
                table: "BooksAdmins",
                column: "AdminIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAdmins_PlantId",
                table: "BooksAdmins",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAdmins_AdminIdFk",
                table: "ExamAdmins",
                column: "AdminIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAdmins_PlantId",
                table: "ExamAdmins",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_LevelIdFk",
                table: "Plants",
                column: "LevelIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SpecialtiesIdFk",
                table: "Plants",
                column: "SpecialtiesIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_TearmId",
                table: "Plants",
                column: "TearmId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsAdmins_AdminIdFk",
                table: "PostsAdmins",
                column: "AdminIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_PostsAdmins_PlantId",
                table: "PostsAdmins",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_True_or_false_ExamIdFk",
                table: "Question_True_or_false",
                column: "ExamIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamIdFk",
                table: "Questions",
                column: "ExamIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAdmins_AdminIdFk",
                table: "VideoAdmins",
                column: "AdminIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAdmins_PlantId",
                table: "VideoAdmins",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "BooksAdmins");

            migrationBuilder.DropTable(
                name: "MessagesUsers");

            migrationBuilder.DropTable(
                name: "PostsAdmins");

            migrationBuilder.DropTable(
                name: "Question_True_or_false");

            migrationBuilder.DropTable(
                name: "UrlWebSite");

            migrationBuilder.DropTable(
                name: "VideoAdmins");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ExamAdmins");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Categores");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Tearms");
        }
    }
}
