
export async function getWordDensity(data) {
    const response = await request({
        method: 'POST',
        url: 'https://localhost:7155/api/CountWordGroups',
        data: data});

    const result = await response.json();
    return result;
}

function request(params) {
    var method = params.method || 'GET';
    var qs = '';
    var body;
    var headers = params.headers || {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    };

    if (['GET', 'DELETE'].indexOf(method) > -1)
      qs = '?' + getQueryString(params.data);
    else // POST or PUT
      body = JSON.stringify(params.data);

    var url = params.url + qs;

    return fetch(url, { method, headers, body });
}

function getQueryString(params) {
    var esc = encodeURIComponent;
    return Object.keys(params)
        .map(k => esc(k) + '=' + esc(params[k]))
        .join('&');
}