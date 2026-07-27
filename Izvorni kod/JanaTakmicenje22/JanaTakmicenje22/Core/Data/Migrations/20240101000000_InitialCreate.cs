using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace JanaTakmicenje22.Core.Data.Migrations
{
    [Migration("20240101000000_InitialCreate")]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postignuca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Emoji = table.Column<string>(nullable: false),
                    RequiredChallenges = table.Column<int>(nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Postignuca", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    XPReward = table.Column<int>(nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Challenges", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    XP = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Streak = table.Column<int>(nullable: false),
                    LastActivityDate = table.Column<DateTime>(nullable: true),
                    TotalChallengesCompleted = table.Column<int>(nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey("FK_Notes_Users_UserId", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPostignuca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    BadgeId = table.Column<int>(nullable: false),
                    EarnedAt = table.Column<DateTime>(nullable: false)
                },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserPostignuca", x => x.Id);
                   table.ForeignKey("FK_UserPostignuca_Users_UserId", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Cascade);
                   table.ForeignKey("FK_UserPostignuca_Postignuca_BadgeId", x => x.BadgeId, "Postignuca", "Id", onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateTable(
                name: "UserChallenges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CompletedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallenges", x => x.Id);
                    table.ForeignKey("FK_UserChallenges_Users_UserId", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_UserChallenges_Challenges_ChallengeId", x => x.ChallengeId, "Challenges", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex("IX_Notes_UserId", "Notes", "UserId");
            migrationBuilder.CreateIndex("IX_UserPostignuca_UserId", "UserPostignuca", "UserId");
            migrationBuilder.CreateIndex("IX_UserPostignuca_BadgeId", "UserPostignuca", "BadgeId");
            migrationBuilder.CreateIndex("IX_UserChallenges_UserId", "UserChallenges", "UserId");
            migrationBuilder.CreateIndex("IX_UserChallenges_ChallengeId", "UserChallenges", "ChallengeId");

            migrationBuilder.InsertData("Postignuca", new[] { "Id", "Name", "Description", "Emoji", "RequiredChallenges" },
                new object[,]
                {
                    { 1, "Početak putovanja", "Završio/la si prvi izazov -`♡´-", "˚₊‧꒰ა 𓂋 ໒꒱ ‧₊˚", 1 },
                    { 2, "Dobar početak", "5 izazova završeno -`♡´-", "₍ᐢ. .ᐢ₎ ₊˚⊹♡", 5 },
                    { 3, "Osvojio si polovinu", "20 izazova završeno -`♡´-", "ᓚ₍⑅^..^₎♡", 20 },
                    { 4, "Blizu si..", "30 izazova završeno -`♡´-", "── ⋆⋅☆⋅⋆ ──", 30 },
                    { 5, "Šampion", "Svi izazovi završeni -`♡´-", "( ˶˘ ³˘)♡", 40 },
                    { 6, "Deset dana! Idemooo", "10 izazova završeno -`♡´-", "✧｡٩(ˊᗜˋ )و✧*｡", 10 }
                });
            migrationBuilder.InsertData("Challenges",
                new[] { "Id", "Title", "Description", "Order", "XPReward" },
                new object[,]
                {
                    { 1, "3", "Zapiši prve tri lepe stvari o sebi koje ti padnu na pamet.", 1, 80 },
                    { 2, "Prazno platno", "Ostavi bilo šta što bi ti poremetilo pažnju sa strane na pola sata, uzmi olovku i papir koji ćeš staviti ispred sebe i samo sedi pola sata. Možda ti čak i padne na pamet da napišeš nešto :)).", 2, 90 },
                    { 3, "Priroda i čovek kao jedno", "Hoću da izdvojiš danas sat vremena, ujutro bi bilo najbolje, i odeš na trčanje ili šetanje, ali bez telefona i slušalica.", 3, 100 },
                    { 4, "Sam sa sobom", "Opet ćemo meditirati, ali ovaj put bez olovke i papira. Budi u tišini. Kad god osetišneku negativnu emociju, stani, vrati se u nazad i pokušaj da razmisliš zašto si je osetio.", 4, 100 },
                    { 5, "Komplimenti", "Priđi nekoj osobi na ulici i udeli im kompliment. Malo teže nego prošli zadaci, ali uzmi u obzir da se niko nikad nije žalio na kompliment :).", 5, 150 },
                    { 6, "Neočekivani poziv", "Umesto da pošalješ poruku, pozovi nekoga da ćaskate. Znam da najradije ne bi ni na poruku odgovorio ali testiraj se malo.", 6, 150 },
                    { 7, "Ne!", "Kreni da vežbaš iskreno odgovarati ljudima. Traže od tebe da uradiš nešto što radije ne bi? Samo kaži ne!", 7, 160 },
                    { 8, "Mala pomoć", "Traži od nekog radnika u prodavnici pomoć za nešto.", 8, 130 },
                    { 9, "Dejt za jednog", "Izađi negde sam. Kafić, bioskop ili restoran? Nije bitno.", 9, 180 },
                    { 10, "Izlazak", "Pozovi nekog na kafu. Iznenadi i ti malo ljude!", 10, 170 },
                    { 11, "Prvi put", "Obrati se uslužnom radniku bez uvežbavanja teksta.", 11, 140 },
                    { 12, "Poruka podrške", "Pošalji poruku nekome koga voliš, samo da im kažeš da misliš na njih.", 12, 120 },
                    { 13, "Izlazak 2", "Ovog puta želim da ideš na neku žurku, klub ili dešavanje gde se okuplja više ljudi.", 13, 200 },
                    { 14, "Bez osećaja sramote", "Jedi na javnom mestu ispred drugih bez osećaja da te neko osuđuje.", 14, 150 },
                    { 15, "Broj", "Pitaj nekoga koga ne poznaješ, a privlačan ti je, za njihov broj telefona.", 15, 250 },
                    { 16, "Small talk", "Dok čekaš u redu za nešto probaj da pokreneš razgovor sa osobom iza sebe.", 16, 160 },
                    { 17, "Bez distrakcija!", "Jedi obrok bez gledanja u ekran ili čitanja bilo čega.", 17, 110 },
                    { 18, "Neutralno lice", "Kada ti se neko obraća nemoj da se smeškaš na nešto sa čim se ne slažeš. Neutralno lice.", 18, 140 },
                    { 19, "Umetnost Disanja", "Fokusiraj se na udisanje i izdisanje 3 minuta dok sediš u parku.", 19, 100 },
                    { 20, "Već smo na pola puta!", "Čestitaj sebi! Na pola si puta. Zapiši šta si naučio o sebi tokom ovih izazova.", 20, 300 },
                    { 21, "Samo pozitiva", "Podeli sa nekim svoj uspeh na koji si ponosan.", 21, 150 },
                    { 22, "Izlazak 3", "Ovog puta ćeš izaći BEZ ikoga, ideš na socijalno okupljanje sam. Verujem u tebe.", 22, 220 },
                    { 23, "Šetnja sa osmehom", "Dok se budeš šetao, želim da se osmehneš petorici ljudi koje budeš sreo usput.", 23, 160 },
                    { 24, "Zelena Terapija", "Provedi pola sata u prirodi ili pored biljaka.", 24, 120 },
                    { 25, "Mala pomoć 2", "Ovog puta ti budi taj koji će da priđe osobi kojoj treba pomoć.", 25, 180 },
                    { 26, "Nema guglanja", "Ako osetiš simptom stresa, nemoj guglati dijagnoze sat vremena.", 26, 130 },
                    { 27, "Glasni smeh", "Pogledaj video koji te uvek nasmeje do suza.", 27, 100 },
                    { 28, "Držanje tela", "Ispravi se i drži glavu visoko dok hodaš ulicom.", 28, 130 },
                    { 29, "Bez izvinjenja", "Pokušaj da provedeš dan bez nepotrebnog izvinjenja za sitnice.", 29, 150 },
                    { 30, "Zdrav život", "Pridruži se nekom klubu za trčanje. Izlaziš iz zone komfora u oba slučaja.", 30, 200 },
                    { 31, "Neuobičajeno", "Kad budeš šetao ponesi sa sobom šolju od kuće ili tanjir iz kojeg ćeš jesti/piti.", 31, 170 },
                    { 32, "Obrnuto", "Hoću šta god negativno da se desilo danas iz njega izvučeš nešto pozitivno, kako god umeo.", 32, 160 },
                    { 33, "Mali poklon", "Kupi sebi neku sitnicu (cvet, olovku) kao nagradu za trud.", 33, 120 },
                    { 34, "3 osobe", "Kada budeš izašao predstavi se bar troje ljudi.", 34, 210 },
                    { 35, "Vizuelizacija", "Zamisli sebe u situaciji koja bi te inače stresirala, ali smirenog.", 35, 150 },
                    { 36, "Razgovor sa sobom", "Obraćaj se sebi danas kao što bi se obraćao/la najboljom prijatelju/ici.", 36, 140 },
                    { 37, "Društvena mreža", "Otključaj svoj profil. Budi otvoren za bilo kakvo osuđivanje.", 37, 200 },
                    { 38, "Pomozi drugome", "Uradi jedno malo dobro delo za nekoga, anonimno.", 38, 180 },
                    { 39, "Samoodbrana", "Ako neko govori ili radi nešto protiv tebe, ne ćuti nego se odbrani!", 39, 220 },
                    { 40, "Bravooooo!!!", "Završio/la si sve predviđene zadatke. Nadam se da si primetio/la neki napredak u odnosu na početak. Budi ponosan/ponosna na sebe!", 40, 500 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UserChallenges");
            migrationBuilder.DropTable("UserPostignuca");
            migrationBuilder.DropTable("Notes");
            migrationBuilder.DropTable("Users");
            migrationBuilder.DropTable("Challenges");
            migrationBuilder.DropTable("Postignuca");
        }
    }
}
