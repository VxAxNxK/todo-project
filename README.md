# Todo 全端專案

一個簡單的待辦事項 (Todo) CRUD 應用，後端使用 ASP.NET Core Web API，前端使用 Angular。此專案作為個人學習 Angular 前端開發的練習專案。

## 技術棧

**後端 (TodoApi)**
- ASP.NET Core Web API
- Entity Framework Core (Code First + Migrations)
- SQL Server
- 分層架構：Controller → Service → Repository → Data (DbContext)

**前端 (todo-client)**
- Angular（standalone components + signals）
- TypeScript
- HttpClient 串接後端 API

## 專案結構

```
Todo/
├── TodoApi/              # 後端 Web API
│   ├── Controller/       # API 進入點
│   ├── Services/         # 商業邏輯
│   ├── Repositories/     # 資料存取
│   ├── Models/           # 資料庫實體 (Todo, Category)
│   ├── DTOs/             # 前後端資料傳輸格式
│   └── Data/             # DbContext
└── todo-client/          # 前端 Angular 專案
    └── src/app/
        ├── models/       # TypeScript interface
        ├── services/     # 呼叫 API 的 Service
        └── todo-list/    # 待辦事項列表元件
```

## 功能

- 新增 / 查詢 / 更新 / 刪除待辦事項
- 待辦事項分類 (Category) 關聯

## 如何啟動

### 後端

```bash
cd TodoApi
dotnet ef database update   # 建立資料庫（第一次執行需要）
dotnet run
```

啟動後終端機會顯示監聽的網址，例如 `http://localhost:5000`。

### 前端

```bash
cd todo-client
npm install
ng serve
```

開啟瀏覽器進入 `http://localhost:4200`。

> 注意：前端 `src/app/services/todo.ts` 裡的 `apiUrl` 需與後端實際監聽的網址、port 一致。

## 開發筆記

- 後端啟用 CORS 允許 `http://localhost:4200` 存取（見 `Program.cs`）
- 開發環境使用 HTTP，未啟用 HTTPS 轉址
- DTO 與 Entity 間為手動轉換 (mapping)，新增欄位時需同步檢查 DTO / Service 轉換邏輯，避免欄位漏傳導致預設值錯誤

## 待辦（下一步）

- [ ] 前端加入 Category 下拉選單，取代目前寫死的 `categoryId`
- [ ] 新增「編輯待辦事項」功能
- [ ] POST 成功時回傳 201 Created 而非 204 No Content
