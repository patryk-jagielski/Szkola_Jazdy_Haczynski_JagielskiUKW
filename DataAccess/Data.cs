using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace DataAccess
{
    public static class Data
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("szkolaJazdy.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableSzkolaJazdy = "CREATE TABLE IF NOT EXISTS \"SzkolaJazdy\" ( 'szkolaJazdyId' INTEGER PRIMARY KEY AUTOINCREMENT, 'nazwa' TEXT NOT NULL, 'ulica' TEXT NOT NULL, 'numerLokalu' INTEGER NOT NULL, 'miejscowosc' TEXT NOT NULL, 'kodPocztowy' TEXT NOT NULL, 'telefon' TEXT NOT NULL, 'email' TEXT NOT NULL )";
                String tableKursanci = "CREATE TABLE IF NOT EXISTS \"Kursanci\" ( 'kursantId' INTEGER PRIMARY KEY AUTOINCREMENT, 'login' TEXT NOT NULL UNIQUE, 'haslo' TEXT NOT NULL, 'imie' TEXT NOT NULL, 'nazwisko' TEXT NOT NULL, 'telefon' TEXT NOT NULL, 'email' TEXT UNIQUE, 'szkolaJazdyId' INTEGER NOT NULL )";
                String tableInstruktorzy = "CREATE TABLE IF NOT EXISTS \"Instruktorzy\" ( 'instruktorId' INTEGER PRIMARY KEY AUTOINCREMENT, 'login' TEXT NOT NULL UNIQUE, 'haslo' TEXT NOT NULL, 'imie' TEXT NOT NULL, 'nazwisko' TEXT NOT NULL, 'telefon' TEXT NOT NULL, 'email' TEXT UNIQUE, 'status' TEXT NOT NULL, 'szkolaJazdyId' INTEGER NOT NULL)";
                String tableDanePojazdow = "CREATE TABLE IF NOT EXISTS \"DanePojazdow\" ( 'danePojazduId' INTEGER PRIMARY KEY AUTOINCREMENT, 'typPojazdu' TEXT NOT NULL, 'marka' TEXT NOT NULL, 'modelPojazdu' TEXT NOT NULL, 'rokProdukcji' TEXT NOT NULL, 'skrzyniaBiegow' TEXT NOT NULL, 'statusPojazdu' TEXT NOT NULL, 'szkolaJazdyId' INTEGER NOT NULL)";
                String tableAdministratorzy = "CREATE TABLE IF NOT EXISTS \"Administratorzy\" ( 'administratorId' INTEGER PRIMARY KEY AUTOINCREMENT, 'login' TEXT NOT NULL UNIQUE, 'haslo' TEXT NOT NULL, 'email' TEXT NOT NULL UNIQUE, 'szkolaJazdyId' INTEGER NOT NULL )";
                String tableHarmJazdKurs = "CREATE TABLE IF NOT EXISTS \"HarmJazdKurs\" ( 'harmKursId' INTEGER PRIMARY KEY AUTOINCREMENT, 'dataJazd' TEXT NOT NULL, 'godzinaJazd' TEXT NOT NULL, 'status' TEXT NOT NULL, 'kursantId' INTEGER NOT NULL, 'instruktorId' INTEGER NOT NULL)";

                SqliteCommand createTableSzkolaJazdy = new SqliteCommand(tableSzkolaJazdy, db);
                SqliteCommand createTableKursanci = new SqliteCommand(tableKursanci, db);
                SqliteCommand createTableInstruktorzy = new SqliteCommand(tableInstruktorzy, db);
                SqliteCommand createTablePojazdy = new SqliteCommand(tableDanePojazdow, db);
                SqliteCommand createTableAdministratorzy = new SqliteCommand(tableAdministratorzy, db);
                SqliteCommand createTableHarmJazdKurs = new SqliteCommand(tableHarmJazdKurs, db);

                createTableSzkolaJazdy.ExecuteReader();
                createTableKursanci.ExecuteReader();
                createTableInstruktorzy.ExecuteReader();
                createTablePojazdy.ExecuteReader();
                createTableAdministratorzy.ExecuteReader();
                createTableHarmJazdKurs.ExecuteReader();
            }
        }
        //KURSANCI
        public async static void AddDataKursanci(string wartoscLogin, string wartoscHaslo, string wartoscImie,
            string wartoscNazwisko, string wartoscTelefon, string wartoscEmail, int wartoscSzkolyId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand insertKursanci = new SqliteCommand();
                    insertKursanci.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertKursanci.CommandText = "INSERT INTO Kursanci (login, haslo, imie, nazwisko, telefon, email, szkolaJazdyId) VALUES " +
                        "(@wartoscLogin, @wartoscHaslo, @wartoscImie, @wartoscNazwisko, @wartoscTelefon, @wartoscEmail, @wartoscSzkolyJazdyId);";
                    insertKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                    insertKursanci.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);
                    insertKursanci.Parameters.AddWithValue("@wartoscImie", wartoscImie);
                    insertKursanci.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);
                    insertKursanci.Parameters.AddWithValue("@wartoscTelefon", wartoscTelefon);
                    insertKursanci.Parameters.AddWithValue("@wartoscEmail", wartoscEmail);
                    insertKursanci.Parameters.AddWithValue("@wartoscSzkolyJazdyId", wartoscSzkolyId);

                    insertKursanci.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie się zarejestowano");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można się zarejestrować. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static List<String> GetDataKursanci()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT * FROM Kursanci", db);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                }

                db.Close();
            }
            return wynikSelectu;
        }
        public async static void UpdateRekordKur(string wartoscLogin, string wartoscHaslo, string wartoscImie,
          string wartoscNazwisko, string wartoscTelefon, string wartoscEmail, string aktualnaWartoscLogin)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand updateIns = new SqliteCommand();
                    updateIns.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    updateIns.CommandText = "UPDATE Kursanci SET login = @wartoscLogin, haslo = @wartoscHaslo, imie = @wartoscImie, " +
                        "nazwisko = @wartoscNazwisko, telefon = @wartoscTelefon, email = @wartoscEmail WHERE login = @aktualnaWartoscLogin;";
                    updateIns.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                    updateIns.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);
                    updateIns.Parameters.AddWithValue("@wartoscImie", wartoscImie);
                    updateIns.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);
                    updateIns.Parameters.AddWithValue("@wartoscTelefon", wartoscTelefon);
                    updateIns.Parameters.AddWithValue("@wartoscEmail", wartoscEmail);
                    updateIns.Parameters.AddWithValue("@aktualnaWartoscLogin", aktualnaWartoscLogin);

                    updateIns.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zaktualizowano dane.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można zaktualizowac danych. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static List<String> GetDataKursanciWhere(string wartoscLogin)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT * FROM Kursanci WHERE login = @wartoscLogin", db);
                selectKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetDataKursanciWhereNaz(string wartoscKursantId)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT * FROM Kursanci WHERE kursantId = @wartoscKursantId", db);
                selectKursanci.Parameters.AddWithValue("@wartoscKursantId", wartoscKursantId);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetDataKursNaz()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT nazwisko FROM Kursanci;", db);
                
                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        //LOGOWANIE KURSANCI
        public static List<String> LoginInDataKursanci(string wartoscLogin, string wartoscHaslo)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT EXISTS( SELECT login, haslo FROM Kursanci WHERE login = @wartoscLogin AND haslo = @wartoscHaslo);", db);
                selectKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                selectKursanci.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }
                db.Close();
            }
            return wynikSelectu;
        }


        //POJAZDY
        public async static void AddDataPojazdy(string wartoscTypPojazdu, string wartoscMarka, string wartoscModelPojazdu,
            string wartoscRokProdukcji, string wartoscSkrzyniaBiegow, string wartoscStatusPojazdu, string wartoscSzkolaJazdyId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand insertPojazdy = new SqliteCommand();
                    insertPojazdy.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertPojazdy.CommandText = "INSERT INTO DanePojazdow (typPojazdu, marka, modelPojazdu, rokProdukcji, skrzyniaBiegow, statusPojazdu, szkolaJazdyId) VALUES " +
                        "(@wartoscTypPojazdu, @wartoscMarka, @wartoscModelPojazdu, @wartoscRokProdukcji, @wartoscSkrzyniaBiegow, @wartoscStatusPojazdu, @wartoscSzkolaJazdyId);";
                    insertPojazdy.Parameters.AddWithValue("@wartoscTypPojazdu", wartoscTypPojazdu);
                    insertPojazdy.Parameters.AddWithValue("@wartoscMarka", wartoscMarka);
                    insertPojazdy.Parameters.AddWithValue("@wartoscModelPojazdu", wartoscModelPojazdu);
                    insertPojazdy.Parameters.AddWithValue("@wartoscRokProdukcji", wartoscRokProdukcji);
                    insertPojazdy.Parameters.AddWithValue("@wartoscSkrzyniaBiegow", wartoscSkrzyniaBiegow);
                    insertPojazdy.Parameters.AddWithValue("@wartoscStatusPojazdu", wartoscStatusPojazdu);
                    insertPojazdy.Parameters.AddWithValue("@wartoscSzkolaJazdyId", wartoscSzkolaJazdyId);

                    insertPojazdy.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie dodano rekord do bazy.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można dodać rekordu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }

            }
        }
        public static List<String> GetDataPojazdy()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectPojazdy = new SqliteCommand
                    ("SELECT * FROM DanePojazdow", db);

                SqliteDataReader query = selectPojazdy.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public async static void UpdateDataPojazdy(string wartoscStatusPojazdu, string wartoscDanePojazdowId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand updatePojazdStatus = new SqliteCommand();
                    updatePojazdStatus.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    updatePojazdStatus.CommandText = "UPDATE DanePojazdow SET statusPojazdu = @wartoscStatusPojazdu WHERE danePojazduId = @wartoscDanePojazdowId;";
                    updatePojazdStatus.Parameters.AddWithValue("@wartoscStatusPojazdu", wartoscStatusPojazdu);
                    updatePojazdStatus.Parameters.AddWithValue("@wartoscDanePojazdowId", wartoscDanePojazdowId);

                    updatePojazdStatus.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zaktualizowano status pojazdu.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można zaktualizowac statusu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public async static void DeleteDataPojazdy(string wartoscDanePojazdowId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand deletePojazd = new SqliteCommand();
                    deletePojazd.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    deletePojazd.CommandText = "DELETE FROM DanePojazdow WHERE danePojazduId = @wartoscDanePojazdowId;";
                    deletePojazd.Parameters.AddWithValue("@wartoscDanePojazdowId", wartoscDanePojazdowId);

                    deletePojazd.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie usunięto pojazd z bazy.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można usunąć pojazdu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }

            }
        }
        //INSTRUKTORZY
        public static List<String> GetDataInstruktorzy()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInstruktorzy = new SqliteCommand
                    ("SELECT * FROM Instruktorzy", db);

                SqliteDataReader query = selectInstruktorzy.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                    wynikSelectu.Add(query.GetString(7));
                    wynikSelectu.Add(query.GetString(8));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetDataInsWhereNaz(string wartoscNazwisko)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInstruktorzy = new SqliteCommand
                    ("SELECT * FROM Instruktorzy WHERE nazwisko = @wartoscNazwisko", db);
                selectInstruktorzy.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);
                SqliteDataReader query = selectInstruktorzy.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                    wynikSelectu.Add(query.GetString(7));
                    wynikSelectu.Add(query.GetString(8));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetNazwiskaInstruktorzy()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInstruktorzy = new SqliteCommand
                    ("SELECT nazwisko FROM Instruktorzy", db);

                SqliteDataReader query = selectInstruktorzy.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetDataInstruktorWhere(string wartoscLogin)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT * FROM Instruktorzy WHERE login = @wartoscLogin", db);
                selectKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                    wynikSelectu.Add(query.GetString(6));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public async static void UpdateRekordIns(string wartoscLogin, string wartoscHaslo, string wartoscImie,
           string wartoscNazwisko, string wartoscTelefon, string wartoscEmail, string aktualnaWartoscLogin)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand updateIns = new SqliteCommand();
                    updateIns.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    updateIns.CommandText = "UPDATE Instruktorzy SET login = @wartoscLogin, haslo = @wartoscHaslo, imie = @wartoscImie, " +
                        "nazwisko = @wartoscNazwisko, telefon = @wartoscTelefon, email = @wartoscEmail WHERE login = @aktualnaWartoscLogin;";
                    updateIns.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                    updateIns.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);
                    updateIns.Parameters.AddWithValue("@wartoscImie", wartoscImie);
                    updateIns.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);
                    updateIns.Parameters.AddWithValue("@wartoscTelefon", wartoscTelefon);
                    updateIns.Parameters.AddWithValue("@wartoscEmail", wartoscEmail);
                    updateIns.Parameters.AddWithValue("@aktualnaWartoscLogin", aktualnaWartoscLogin);

                    updateIns.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zaktualizowano dane.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można zaktualizowac danych. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public async static void AddDataInstruktorzy(string wartoscLogin, string wartoscHaslo, string wartoscImie,
            string wartoscNazwisko, string wartoscTelefon, string wartoscEmail, string wartoscStatus, string wartoscSzkolaJazdyId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand insertInstruktorzy = new SqliteCommand();
                    insertInstruktorzy.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertInstruktorzy.CommandText = "INSERT INTO Instruktorzy (login, haslo, imie, nazwisko, telefon, email, status, szkolaJazdyId) VALUES " +
                        "(@wartoscLogin, @wartoscHaslo, @wartoscImie, @wartoscNazwisko, @wartoscTelefon, @wartoscEmail, @wartoscStatus, @wartoscSzkolaJazdyId);";
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscImie", wartoscImie);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscTelefon", wartoscTelefon);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscEmail", wartoscEmail);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscStatus", wartoscStatus);
                    insertInstruktorzy.Parameters.AddWithValue("@wartoscSzkolaJazdyId", wartoscSzkolaJazdyId);

                    insertInstruktorzy.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie dodano rekord do bazy.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można dodać rekordu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public async static void UpdateDataStatusIns(string wartoscStatusIns, string wartoscInstruktorId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand updateInstruktorStatus = new SqliteCommand();
                    updateInstruktorStatus.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    updateInstruktorStatus.CommandText = "UPDATE Instruktorzy SET status = @wartoscStatusIns WHERE instruktorId = @wartoscInstruktorId;";
                    updateInstruktorStatus.Parameters.AddWithValue("@wartoscStatusIns", wartoscStatusIns);
                    updateInstruktorStatus.Parameters.AddWithValue("@wartoscInstruktorId", wartoscInstruktorId);

                    updateInstruktorStatus.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zaktualizowano status instruktora.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można zaktualizowac statusu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public async static void DeleteDataIns(string wartoscInstruktorId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand deleteInstruktor = new SqliteCommand();
                    deleteInstruktor.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    deleteInstruktor.CommandText = "DELETE FROM Instruktorzy WHERE instruktorId = @wartoscInstruktorId;";
                    deleteInstruktor.Parameters.AddWithValue("@wartoscInstruktorId", wartoscInstruktorId);

                    deleteInstruktor.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie usunięto instruktora z bazy.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można usunąć instruktora. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        //LOGOWANIE INSTRUKTORZY
        public static List<String> LoginInDataInstruktorzy(string wartoscLogin, string wartoscHaslo)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT EXISTS( SELECT login, haslo FROM Instruktorzy WHERE login = @wartoscLogin AND haslo = @wartoscHaslo);", db);
                selectKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                selectKursanci.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }
                db.Close();
            }
            return wynikSelectu;
        }

        //LOGOWANIE Admin
        public static List<String> LoginInDataAdmin(string wartoscLogin, string wartoscHaslo)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT EXISTS( SELECT login, haslo FROM Administratorzy WHERE login = @wartoscLogin AND haslo = @wartoscHaslo);", db);
                selectKursanci.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                selectKursanci.Parameters.AddWithValue("@wartoscHaslo", wartoscHaslo);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }
                db.Close();
            }
            return wynikSelectu;
        }
        //JAZDY

        public async static void AddDataHarmJazdKurs(string wartoscDataJazd, string wartoscGodzinaJazd, string wartoscStatus, string wartoscKursantId,
           string wartosInstruktorId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand insertHarmJazdKurs = new SqliteCommand();
                    insertHarmJazdKurs.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertHarmJazdKurs.CommandText = "INSERT INTO HarmJazdKurs (dataJazd, godzinaJazd, status, kursantId, instruktorId) VALUES " +
                        "(@wartoscDataJazd, @wartoscGodzinaJazd, @wartoscStatus, @wartoscKursantId, @wartosInstruktorId);";
                    insertHarmJazdKurs.Parameters.AddWithValue("@wartoscDataJazd", wartoscDataJazd);
                    insertHarmJazdKurs.Parameters.AddWithValue("@wartoscGodzinaJazd", wartoscGodzinaJazd);
                    insertHarmJazdKurs.Parameters.AddWithValue("@wartoscStatus", wartoscStatus);
                    insertHarmJazdKurs.Parameters.AddWithValue("@wartoscKursantId", wartoscKursantId);
                    insertHarmJazdKurs.Parameters.AddWithValue("@wartosInstruktorId", wartosInstruktorId);

                    insertHarmJazdKurs.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zapisano się na jazdy.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static List<String> GetIdInstruktorWhere(string wartoscNazwisko)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInstruktorId = new SqliteCommand
                    ("SELECT instruktorId FROM Instruktorzy WHERE nazwisko = @wartoscNazwisko", db);
                selectInstruktorId.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);

                SqliteDataReader query = selectInstruktorId.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetIdKursantWhere(string wartoscLogin)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanciId = new SqliteCommand
                    ("SELECT kursantId FROM Kursanci WHERE login = @wartoscLogin", db);
                selectKursanciId.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);

                SqliteDataReader query = selectKursanciId.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetIdKursantWhereNaz(string wartoscNazwisko)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanciId = new SqliteCommand
                    ("SELECT kursantId FROM Kursanci WHERE nazwisko = @wartoscNazwisko", db);
                selectKursanciId.Parameters.AddWithValue("@wartoscNazwisko", wartoscNazwisko);

                SqliteDataReader query = selectKursanciId.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }

            return wynikSelectu;
        }
        public static List<String> GetHarmJazdWhere(string wartoscKursantId)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursantHarm = new SqliteCommand
                    ("SELECT * FROM HarmJazdKurs WHERE kursantId = @wartoscKursantId;", db);
                selectKursantHarm.Parameters.AddWithValue("@wartoscKursantId", wartoscKursantId);
                SqliteDataReader query = selectKursantHarm.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                }

                db.Close();
            }
            return wynikSelectu;
        }
        public static List<String> GetHarmJazdWhereIns(string wartoscInstruktorId)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInstruktorHarm = new SqliteCommand
                    ("SELECT * FROM HarmJazdKurs WHERE instruktorId = @wartoscInstruktorId;", db);
                selectInstruktorHarm.Parameters.AddWithValue("@wartoscInstruktorId", wartoscInstruktorId);
                SqliteDataReader query = selectInstruktorHarm.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                }

                db.Close();
            }
            return wynikSelectu;
        }
        public static List<String> GetInsIdWhereLogin(string wartoscLogin)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectInsId = new SqliteCommand
                    ("SELECT instruktorId FROM Instruktorzy WHERE login = @wartoscLogin;", db);
                selectInsId.Parameters.AddWithValue("@wartoscLogin", wartoscLogin);
                SqliteDataReader query = selectInsId.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }

                db.Close();
            }
            return wynikSelectu;
        }
        public static List<String> GetHarmJazd()
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanciHarm = new SqliteCommand
                    ("SELECT * FROM HarmJazdKurs", db);
                SqliteDataReader query = selectKursanciHarm.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                    wynikSelectu.Add(query.GetString(1));
                    wynikSelectu.Add(query.GetString(2));
                    wynikSelectu.Add(query.GetString(3));
                    wynikSelectu.Add(query.GetString(4));
                    wynikSelectu.Add(query.GetString(5));
                }

                db.Close();
            }
            return wynikSelectu;
        }
        public async static void HarmJazdStatus(string wartoscStatus, string wartoscHarmKursId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                try
                {
                    db.Open();

                    SqliteCommand updateInstruktorStatus = new SqliteCommand();
                    updateInstruktorStatus.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    updateInstruktorStatus.CommandText = "UPDATE HarmJazdKurs SET status = @wartoscStatus WHERE harmKursId = @wartoscHarmKursId;";
                    updateInstruktorStatus.Parameters.AddWithValue("@wartoscStatus", wartoscStatus);
                    updateInstruktorStatus.Parameters.AddWithValue("@wartoscHarmKursId", wartoscHarmKursId);

                    updateInstruktorStatus.ExecuteReader();

                    var messageDialog = new Windows.UI.Popups.MessageDialog("Pomyślnie zaktualizowano status.");
                    await messageDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("Błąd, nie można zaktualizowac statusu. " + ex.ToString());
                    await messageDialog.ShowAsync();
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static List<String> CheckStatusJazd(string wartoscKursantId)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT EXISTS( SELECT status FROM HarmJazdKurs WHERE status = 'Nieukończone' AND kursantId = @wartoscKursantId);", db);
                selectKursanci.Parameters.AddWithValue("@wartoscKursantId", wartoscKursantId);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }
                db.Close();
            }
            return wynikSelectu;
        }
        public static List<String> CheckDateAndHour(string wartoscInstruktorId, string wartoscDataJazd, string wartoscGodzinaJazd)
        {
            List<String> wynikSelectu = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "szkolaJazdy.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectKursanci = new SqliteCommand
                    ("SELECT EXISTS( SELECT dataJazd, godzinaJazd FROM HarmJazdKurs WHERE (dataJazd = @wartoscDataJazd AND godzinaJazd = @wartoscGodzinaJazd) AND instruktorId = @wartoscInstruktorId);", db);
                selectKursanci.Parameters.AddWithValue("@wartoscDataJazd", wartoscDataJazd);
                selectKursanci.Parameters.AddWithValue("@wartoscGodzinaJazd", wartoscGodzinaJazd);
                selectKursanci.Parameters.AddWithValue("@wartoscInstruktorId", wartoscInstruktorId);

                SqliteDataReader query = selectKursanci.ExecuteReader();

                while (query.Read())
                {
                    wynikSelectu.Add(query.GetString(0));
                }
                db.Close();
            }
            return wynikSelectu;
        }
    }
}
