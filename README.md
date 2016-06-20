# ProgramisciElista - projekt na fakultet

Dokumentacja aplikacji restowej.

Kontrollery:

 1. User - Akcje dostępne dla użytkownika
 2. Diary - Akcje dostępu do planowanych zadań
 3. Leader - Akcje dla lidera zespołu
 4. Postman - Wystawia api dla postmana
 5. Profile - Obsługa logowania, edycji profilu
 6. Admin - Akcje administratora
 
 Akcje kontrolerów:

        "requests":[  
          {  
            "comment":"Aktualizuje profil zalogowanego użytkownika",
             "name":"Profile/Update",
             "url":"http://localhost:50000/Profile/Update",
             "method":"POST",
             "data":"{\"Lastname\":null,\"Firstname\":null,\"TechDate\":\"0001-01-01T00:00:00\",\"Email\":null,\"IsActivated\":false}"
          },
          { 
          "comment":"Zmienia hasło aktualnie zalogowanego użytkownika",
             "name":"Profile/ChangePassword",
             "url":"http://localhost:50000/Profile/ChangePassword",
             "method":"POST",
             "data":"{\"OldPassword\":null,\"OldPasswordSecond\":null,\"NewPassword\":null}"
          },
          { 
          "comment":"Logowanie do aplikacji, zwraca token sesji który należy przekazywać w nagłówki session z każdym reqeuestem",
             "name":"Profile/Login",
             "url":"http://localhost:50000/Profile/Login",
             "method":"POST",
             "data":"{\"Email\":null,\"Password\":null}"
          },
          {  
          "comment":"Wylogowanie, msg pozwala dodać ostateczną wiadomosć do Work Loga",
             "name":"Profile/Logout?msg={msg}",
             "url":"http://localhost:50000/Profile/Logout?msg={msg}",
             "method":"POST"
          },
          {  
          "comment":"Lista uzytkowników, pageowana, szukanie po polu",
             "name":"User/List?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "url":"http://localhost:50000/User/List?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "method":"GET"
          },
          { 
          "comment":"Plan zalogowanego użytkownika",
             "name":"User/MyPlan",
             "url":"http://localhost:50000/User/MyPlan",
             "method":"GET",
          },
          {  
          "comment":"Logi aktualnie zalogowanego użytkownika",
             "name":"User/MyLogs",
             "url":"http://localhost:50000/User/MyLogs",
             "method":"GET"
          },
          {  
          "comment":"Logowanie aktywności (do teraz robiłem coś)",
             "name":"User/LogWork?msg={msg}",
             "url":"http://localhost:50000/User/LogWork?msg={msg}",
             "method":"PUT"
          },
          {  
          "comment":"Tworzenie drużyny jako lider",
             "name":"Leader/Create",
             "url":"http://localhost:50000/Leader/Create",
             "method":"POST",
             "data":"{\"Members\":null,\"TeamName\":null}"
          },
          {  
          "comment":"Dodawanie osób do drużyny",
             "name":"Leader/Add",
             "url":"http://localhost:50000/Leader/Add",
             "method":"POST",
             "data":"{\"Members\":null,\"TeamName\":null}"
          },
          {  
          "comment":"Update nazwy druzyny",
             "name":"Leader/UpdateName?name={name}",
             "url":"http://localhost:50000/Leader/UpdateName?name={name}",
             "method":"PUT"
          },
          {  
          "comment":"Usuwanie osób z grupy",
             "name":"Leader/Remove",
             "url":"http://localhost:50000/Leader/Remove",
             "method":"POST",
             "data":"{\"Members\":null}"
          },
          {  
          "comment":"Pobranie wszystkich osób z drużyny",
             "name":"Leader/GetTeam",
             "url":"http://localhost:50000/Leader/GetTeam",
             "method":"GET"
          },
          {  
          "comment":"Pobieranie planu dla użytkownika",
             "name":"Diary/GetUserPlans?userId={userId}",
             "url":"http://localhost:50000/Diary/GetUserPlans?userId={userId}",
             "method":"GET"
          },
          { 
          "comment":"Tworzenie planu dla użytkownika",
             "name":"Diary/CreatePlan",
             "url":"http://localhost:50000/Diary/CreatePlan",
             "method":"POST"
             "data":"{\"UserId\":0,\"Start\":\"0001-01-01T00:00:00\",\"End\":\"0001-01-01T00:00:00\",\"Day\":0,\"JobInfo\":null}"
          },
          {  
          "comment":"Dezaktywacja planu uzytkownika",
             "name":"Diary/DeactivatePlan/{id}",
             "url":"http://localhost:50000/Diary/DeactivatePlan/{id}",
             "method":"PUT"
          },
          {  
          "comment":"Pobieranie aktualnie zalogowanych użytkowników",
             "name":"Admin/GetUsersActivity",
             "url":"http://localhost:50000/Admin/GetUsersActivity",
             "method":"GET"
          },
          {  
          "comment":"Lista użytkowników",
             "name":"Admin/List?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "url":"http://localhost:50000/Admin/List?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "method":"GET"
          },
          {  
          "comment":"Lista drużyn",
             "name":"Admin/TeamsList?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "url":"http://localhost:50000/Admin/TeamsList?pageSize={pageSize}&page={page}&searchByField={searchByField}&fieldValue={fieldValue}",
             "method":"POST"
          },
          {  
          "comment":"Aktywowanie użytkownika",
             "name":"Admin/ActivateUser/{id}",
             "url":"http://localhost:50000/Admin/ActivateUser/{id}",
             "method":"PUT"
          },
          {  
          "comment":"Dezaktywowanie użytkownika",
             "name":"Admin/DeactivateUser/{id}",
             "url":"http://localhost:50000/Admin/DeactivateUser/{id}",
             "method":"PUT"
          },
          {  
          "comment":"Tworzenie użytkownika",
             "name":"Admin/Create",
             "url":"http://localhost:50000/Admin/Create",
             "method":"POST",
             "data":"{\"Password\":null,\"Group\":null,\"Lastname\":null,\"Firstname\":null,\"TechDate\":\"0001-01-01T00:00:00\",\"Email\":null,\"IsActivated\":false}"
          },
          {  
          "comment":"Aktualizacja użytkownika",
             "name":"Admin/Update",
             "url":"http://localhost:50000/Admin/Update",
             "method":"POST"
             "data":"{\"Id\":0,\"GroupName\":null,\"Lastname\":null,\"Firstname\":null,\"TechDate\":\"0001-01-01T00:00:00\",\"Email\":null,\"IsActivated\":false}"
          }
       ]
