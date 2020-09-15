export function buildTable(cols: number, arr: any[]) {
    if (!arr)
        return [];

    var result = [];
    var line = [];
    for (var i = 0; i < arr.length; i++) {
        line.push(arr[i]);

        if (line.length >= cols || ((i + 1) >= arr.length)) {
            result.push(line);
            line = [];
        }
    }
    return result;
}