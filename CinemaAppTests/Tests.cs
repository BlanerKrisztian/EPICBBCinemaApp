using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaApp;

namespace CinemaApp.Tests
{
    [TestClass]
    public class ScreeningTests
    {
        private Screening CreateDefaultScreening() => new Screening("Inception", 5);

        // ---- Constructor ----

        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var screening = new Screening("Inception", 100);
            Assert.AreEqual("Inception", screening.GetTitle());
            Assert.AreEqual(100, screening.GetAvailableSeats());
        }
        // TODO: null vagy üres cím esetén ArgumentException-t kell dobni
        // TODO: totalSeats értéke 0 vagy negatív esetén ArgumentException-t kell dobni

        // ---- BookSeat ----

        [TestMethod]
        public void BookSeat_AvailableSeat()
        {
            var screening = CreateDefaultScreening();
            bool result = screening.BookSeat("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(4, screening.GetAvailableSeats());
        }
        // TODO: teli vetítésnél újabb foglalás false-t kell hogy visszaadjon
        // TODO: ugyanaz a személy kétszer próbál foglalni, másodszor false-t kell kapni
        // TODO: több különböző személy foglalása után a szabad helyek száma helyesen csökken

        // ---- CancelBooking ----

        [TestMethod]
        public void CancelBooking_ExistingBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            bool result = screening.CancelBooking("Alice");
            Assert.IsTrue(result);
            Assert.AreEqual(5, screening.GetAvailableSeats());
        }
        // TODO: nem létező foglalás lemondásakor false-t kell visszaadni
        // TODO: lemondás után a személy neve már nem szerepel a foglaltak között

        // ---- IsBooked ----

        [TestMethod]
        public void IsBooked_AfterBooking()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            Assert.IsTrue(screening.IsBooked("Alice"));
        }
        // TODO: foglalás nélküli személyre false-t kell visszaadni
        // TODO: lemondás után ugyanarra a személyre false-t kell visszaadni

        // ---- GetAvailableSeats ----

        [TestMethod]
        public void GetAvailableSeats_AfterMultipleBookings()
        {
            var screening = CreateDefaultScreening(); // 5 férőhely
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(3, screening.GetAvailableSeats());
        }
        // TODO: újonnan létrehozott vetítésnél a szabad helyek száma egyenlő a totalSeats értékével
        // TODO: teli vetítésnél GetAvailableSeats() nullát kell visszaadni

        // ---- GetBookedCount ----

        [TestMethod]
        public void GetBookedCount_AfterBookings()
        {
            var screening = CreateDefaultScreening();
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.AreEqual(2, screening.GetBookedCount());
        }
        // TODO: újonnan létrehozott vetítésnél GetBookedCount() nullát kell visszaadni
        // TODO: lemondás után a foglaltak száma helyesen csökken

        // ---- IsHouseFull ----

        [TestMethod]
        public void IsHouseFull_WhenAllSeatsBooked()
        {
            var screening = new Screening("Inception", 2);
            screening.BookSeat("Alice");
            screening.BookSeat("Bob");
            Assert.IsTrue(screening.IsHouseFull());
        }
        // TODO: szabad hellyel rendelkező vetítésnél false-t kell visszaadni
        // TODO: lemondás után a vetítés már nem teli, IsHouseFull() false-t ad vissza

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista tesztek
        // Az alábbi teszteket csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        // [TestMethod]
        // public void AddToWaitingList_WhenFull()
        // {
        //     ...
        // }
        // TODO (extra): szabad hely esetén várólistára kerülés false-t kell hogy visszaadjon
        // TODO (extra): már foglalással rendelkező személy várólistára kerülése false-t kell hogy visszaadjon
        // TODO (extra): ugyanaz a személy kétszer próbál várólistára kerülni, másodszor false-t kap
        // TODO (extra): lemondás után az első várólistás személy automatikusan foglalást kap
        // TODO (extra): GetWaitingPosition nem létező személyre -1-et kell visszaadni
    }
}
