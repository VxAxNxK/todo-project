# Todo 專案開發日誌

> 說明：這個專案在 2026-07-21 才 `git init`，所以 2026-07-21 之前沒有 commit 紀錄可查。
> 以下「git 之前」的日期是依照 `TodoApi/Migrations/` 底下 EF Core migration 檔名中
> 內建的時間戳記（產生 migration 當下的實際時間）回推的，並非憑印象瞎猜；
> 內容則是我（Claude）直接讀程式碼結構推測出來的，「卡關處」「解法／學到」目前留空，
> 之後可以再憑印象補上。2026-07-21 之後才開始有真正的 git commit 紀錄可以對照。

---

## 2026-06-03 — 後端專案骨架 + Todo/Category 基本 CRUD
- 做了什麼：（依 EF Migration 推測）
  - 建立 ASP.NET Core Web API 專案（TodoApi，.NET 9），採 Controller → Service →
    Repository → Data（DbContext）分層架構。
  - 用 EF Core Code First 建第一個 migration `InitialCreate`：只有一張 `Todos` 表
    （`Id`、`Title`、`IsCompleted`）。
  - 同一天內又加了兩個 migration：
    - `AddCategory`：新增 `Category` 表，並在 `Todos` 加上 `CategoryId` 欄位。
    - `UpdateTodo`：把 `Category` 表改名成 `Categories`（含 PK/FK 一併重建），
      推測是命名不一致（單複數）被發現後立刻修正。
  - 對應建立了 `TodoController` / `CategoryController`、`TodoService` /
    `CategoryService`、`TodoRepository` / `CategoryRepository`、`TodoDTO` /
    `CategoryDTO`，以及 `ApiResponse<T>` 統一回應格式。
- 卡關處：無
- 解法／學到：(待補充)

## 2026-06-08 — 幫 Todo 加上到期日欄位
- 做了什麼：（依 EF Migration 推測）
  - 新增 migration `AddDueDateToTodo`，在 `Todos` 表加上可為 null 的
    `DueDate`（`datetime2`）欄位。
- 卡關處：無
- 解法／學到：(待補充)

## 2026-06-17 — 把到期日欄位拿掉
- 做了什麼：（依 EF Migration 推測）
  - 新增 migration `DeleteTodoDueDate`，把六月八日才加的 `DueDate` 欄位砍掉。
  - 目前程式碼（`Todo.cs`、`TodoDTO`、前端 `models/todo.ts`）都已經完全沒有
    `DueDate` 的痕跡，看起來是確定不需要這個欄位而整個回退，不是改名或搬到別的地方。
- 卡關處：無
- 解法／學到：(待補充)

## （git 之前，確切日期不明）— 前端 Angular 專案 + JWT 驗證骨架
- 做了什麼：（依現有程式碼推測，無時間戳可考，只能確定發生在 2026-07-21 建 git 之前）
  - 建立 `todo-client`（Angular 22，standalone components + signals），有
    `TodoService`（呼叫 `http://localhost:5000/api/todos`）與 `TodoList`
    元件（新增／打勾完成／刪除，畫面用 `@for` 顯示清單）。
  - 後端 `Program.cs` 已經註冊了 JWT Bearer 驗證（`Jwt:Key/Issuer/Audience`
    設定、`AddAuthentication` + `AddJwtBearer`）與 CORS（只放行
    `http://localhost:4200`），但目前專案裡沒有對應的登入/簽發 token 的
    Controller，所以 JWT 驗證目前應該還是「裝好但没有實際被用到」的狀態。
- 卡關處：(待補充)
- 解法／學到：(待補充)

---

## 2026-07-21 — 建立 Git 版控，Initial commit
- 做了什麼：（依 git log 紀錄）
  - `git init` 並建立第一個、也是目前唯一一個 commit「Initial commit」
    （2026-07-21 15:14:03 +0800），一次把前面所有離線開發的成果
    （TodoApi 後端 + todo-client 前端，共 67 個檔案、約 11,751 行）收進版控。
  - 目前 `git log` 就只有這一筆記錄，所以再往前沒有更細的 commit 可以拆分。
- 卡關處：(待補充)
- 解法／學到：之後的修改建議拆成多個小 commit 再送出，這樣以後回頭看
  才能還原「一個功能一個 commit」的開發過程，而不會又變成一次性大包 commit。

## 2026-07-22 — 修正前端 TodoService 測試檔（尚未 commit）
- 做了什麼：（依目前 working tree 的未提交變更）
  - `todo-client/src/app/services/todo.spec.ts` 原本 import/inject 的是
    `Todo`，但 `todo.ts` 實際 export 的類別是 `TodoService`（`Todo` 根本不存在
    於這個檔案），推測是先前用 Angular CLI 產生 service 後把類別改名成
    `TodoService`，但測試檔忘記同步更新。這次把 spec 檔的 import、`describe`
    名稱、變數型別都改成對應 `TodoService`。
- 卡關處：(待補充)
- 解法／學到：(待補充)

## 2026-07-22 — 新增 /devlog 自訂指令
- 做了什麼：
  - 新增自訂 slash command，讓 Claude Code 裡輸入 `/devlog` 就能觸發：自動看
    最新一筆 git commit，幫忙在 `DEVLOG.md` 補上一筆開發紀錄。
- 卡關處：
  - commit 當下把指令內容放在 `.claude/devlog.md`，但 Claude Code 的自訂指令
    實際上要放在 `.claude/commands/` 資料夾底下才會被辨識，所以 commit 完後
    又在 working tree 把檔案搬到 `.claude/commands/devlog.md`（尚未 commit）。
- 解法／學到：自訂 slash command 檔案要放在 `.claude/commands/<name>.md`，
  檔名就是指令名稱；下次新增指令記得一開始就放對資料夾。
