

const loginKey = "DOMAINLOGIN";
const contextIdKey = "contextId";
const userContextIdKey = "userContextId";
const userLoginKey = "userLogin";

const userIdKey = "userId";
const nameKey = "name";
const descriptionKey = "description";

const currentIntervalTimeKey = null;
const RequestType = {Async: "Async", Sync: "Sync"};
const lastClick = {Contexts: null, Requests: null};
let isAscendingOrder = true;
const contextsCondition = () => sessionStorage.getItem(contextIdKey) !== null;
const killContextCondition = (userContextId) => sessionStorage.getItem(contextIdKey) !== null && userContextId !== sessionStorage.getItem(contextIdKey);
const requestsCondition = () => sessionStorage.getItem(contextIdKey) !== null && currentUserContextId() !== null;
const requestTypeEnumToString = (requestType) => requestType.includes(RequestType.Async) ? 'асинхронный' : 'синхронный';
const requestTypeStringToEnum = (requestType) => requestType.includes('асинхронный') ? RequestType.Async : RequestType.Sync;
const currentUserContextId = () => document.getElementById(userContextIdKey).innerHTML;

window.onload = async (event) => {
    ShowPopup();
    if (event.target.location.pathname.includes("index")) {
        await UsersRequest();
    }
    if (event.target.location.pathname.includes("profile")) {
        SetProfileItem();
        await UserRequestsRequest(false);
        //AddEventSorting();
        //StartUserRequestsInterval();
    }
}
function ShowPopup() {
    let modal = document.getElementById("modalSignin");
    if (document.cookie.indexOf('BookMyWeek.Authentication') === 0) {
        modal.style.display = "none";
    } else {
        modal.style.display = "block";
    }
}

async function UsersRequest() {
        await TableRequest("/api/userDatabase/all")
            .then((json) => {
                let table = '<table class="table table-hover" id="users">'
                table += `<tr><th>пользователь</th><th>описание</th><th>планирование</th></tr>`;
                json.forEach((user) => {
                    table += `<tr><td>${user.name}</td><td>${user.description}</td><th><input type="button" onclick="OpenProfile(\'${user.userId}\',\'${user.name}\', \'${user.description}\')" style="text-decoration: none" value=">>" class="btn btn-danger"/></tr>`;
                });
                table += '</table>';
                ApplySorting(table);
            })
}

function OpenProfile(userid, name, description) {
    sessionStorage.setItem(userIdKey, userid);
    sessionStorage.setItem(nameKey, name);
    sessionStorage.setItem(descriptionKey, description);
    window.open(window.location.origin + '/book-my-week/profile.html');
}

function Authentication() {
    let authenticationBody = {
        userName: document.getElementById("username").value,
        password: document.getElementById("password").value
    };
    fetch(window.location.origin + "/api/authentication/login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'text/plain'
        },
        body: JSON.stringify(authenticationBody)
    }).then(response => {
        if (response.ok) {
            response.json().then(data => {
                window.location.reload();
            })
        } else {
            response.json().then(data => {
                document.getElementById("authResult").innerHTML = data.title;
            })

        }
        document.getElementById("authForm").reset();
    });
}
function ApplySorting(table) {
    let tableRange = document.createRange().createContextualFragment(table);
    if (document.getElementById("table").hasChildNodes()) {
        document.getElementById("table").replaceChildren(tableRange);
    } else {
        document.getElementById("table").appendChild(tableRange);
    }
    AddEventSorting();
}

async function UserRequestsRequest(useSort) {
    if (requestsCondition()) {
        await TableRequest("/api/management/requests/" + currentUserContextId())
            .then((json) => {
                let table = '<table class="table table-bordered" id="requests">';
                table += `<tr><th>01</th><th>02</th><th>03</th><th>04</th><th>05</th><th>06</th><th>07</th><th>08</th><th>09</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th></tr>`;

                json.forEach((request) => {
                    table += `<tr><td>${request.requestId}</td><td>${new Date(request.startRequest).toISOString().substring(0, 19).replace("T", "\n")} </td><td>${request.duration.substring(0, 11)}</td><td>${JSON.stringify(request.command)}<br><br>${request.sql}</td> <td>${type}</td><td>${request.database}</td><th><input type="button" onclick="CancelUserRequests(\'${request.requestId}\',\'${request.requestType}\')" style="text-decoration: none" value="X" class="btn btn-danger"/></th></tr>`;
                });
                table += '</table>';
                ApplySorting(useSort, table, "contexts");
            })
    }
}

function KillUserContext(contextId) {
    if (killContextCondition(contextId)) {
        fetch(window.location.origin + "/api/contexts/" + contextId, {
            method: 'Delete', headers: {
                "Content-Type": "application/json"
            }
        }).then(UsersRequest())
    }
}

function CancelUserRequests(requestId, requestType) {
    if (requestsCondition()) {
        fetch(window.location.origin + "/api/management/requests", {
            method: 'Delete', headers: {
                "context-id": sessionStorage.getItem(contextIdKey),
                "Content-Type": "application/json"
            }, body: JSON.stringify([{"RequestId": requestId, "IsAsyncRequest": requestType}])
        }).then(UserRequestsRequest(true))
    }
}

function CancelAllUserRequests() {
    if (requestsCondition()) {
        fetch(window.location.origin + "/api/management/requests", {
            method: 'Delete', headers: {
                "context-id": sessionStorage.getItem(contextIdKey),
                "Content-Type": "application/json"
            }, body: GetBodyForCancelUserRequests()
        }).then(async () => await UserRequestsRequest(false))
    }
}

function GetBodyForCancelUserRequests() {
    let table = document.getElementById("requests");
    let userContextId = currentUserContextId();
    let length = table.rows.length;
    let result = [];
    for (let i = 1; i < length; i++) {
        let tr = table.rows[i];
        let requestId = tr.cells[0].innerHTML;
        let requestType = tr.cells[4].innerHTML;
        result.push({"ContextId": userContextId, "RequestId": requestId, "IsAsyncRequest": requestTypeStringToEnum(requestType)})
    }
    return JSON.stringify(result);
}

function SetProfileItem() {
        document.getElementById(nameKey).innerHTML = sessionStorage.getItem(nameKey);
        document.getElementById(descriptionKey).innerHTML = sessionStorage.getItem(descriptionKey);
}

function SetContextRequestItem() {
    if (contextsCondition()) {
        if (sessionStorage.getItem(currentIntervalTimeKey) !== null)
            document.getElementById("contextsInterval").selectedIndex = GetIndexIntervalSelectorIndex();
        document.getElementById("adminLogin").innerHTML = sessionStorage.getItem(loginKey);
    }
}

function OpenReqestsPage(userContextId, userLogin) {
    sessionStorage.setItem(userContextIdKey, userContextId);
    sessionStorage.setItem(userLoginKey, userLogin);
    window.open(window.location.origin + '/servicemonitor/requests.html');
}

function Search(tableId) {
    let input, filter, table, tr, td, i, txtValue, selectorIndex;
    input = document.getElementById("searchInput")
    filter = input.value.toUpperCase();
    table = document.getElementById(tableId);
    tr = table.getElementsByTagName("tr");
    selectorIndex = 0;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[selectorIndex];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function HandleRequestException(response) {
    if (response.status === 401) {
        sessionStorage.removeItem(loginKey);
        sessionStorage.removeItem(contextIdKey);
        ShowPopup();
    }
}

function CheckConsistencyWithSearch(tableId, lineString) {
    let selectorIndex, filter, input, linevalue;
    input = document.getElementById("searchInput")
    filter = input.value.toUpperCase();
    selectorIndex = document.getElementById("tableSelector").selectedIndex;
    linevalue = Object.values(lineString)[selectorIndex];
    return linevalue.toUpperCase().indexOf(filter) > -1;
}

const getCellValue = (tr, idx) => tr.children[idx].innerText || tr.children[idx].textContent;

const comparer = (idx, asc) => (a, b) => ((v1, v2) =>
        v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2) ? v1 - v2 : v1.toString().localeCompare(v2)
)(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));


function AddEventSorting() {
    document.querySelectorAll('th').forEach(th => {
        let sortingHandler = SortingHandler(th);
        th.removeEventListener('click', sortingHandler);
        th.addEventListener('click', sortingHandler);
    }, false);
}

function SortingHandler(th) {
    return function (event) {
        event.stopImmediatePropagation();
        const table = th.closest('table');
        SetLastClick(table, th);
        isAscendingOrder = !isAscendingOrder;
        new Sort(table, th);
    }
}

function SetLastClick(table, th) {
    switch (table.id) {
        case "contexts":
            lastClick.Contexts = th;
            break;
        case "requests":
            lastClick.Requests = th;
            break;
        default:
    }
}

function Sort(table, th) {
    Array.from(table.querySelectorAll('tr:nth-child(n+2)'))
        .sort(comparer(Array.from(th.parentNode.children).indexOf(th), isAscendingOrder))
        .forEach(tr => table.appendChild(tr));
}

function GetIndexIntervalSelectorIndex() {
    let value = Number(sessionStorage.getItem(currentIntervalTimeKey));
    switch (value) {
        case 0:
            return 0;
        case 5000:
            return 1;
        case 15000:
            return 2;
        case 30000:
            return 3;
        case 60000:
            return 4;
        case 300000:
            return 5;
    }
}

async function TableRequest(path) {
    let response = await fetch(window.location.origin + path, {
        method: 'Get', headers: {
            "Content-Type": "application/json"
        },
    });
    return response.ok ? await response.json() : HandleRequestException(response);
}

