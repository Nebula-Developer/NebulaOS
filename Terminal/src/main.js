const {
    contextBridge,
    ipcRenderer
} = require("electron");

var term = new Terminal();
term.open(document.getElementById('terminal'));
term.write('Hello from \x1B[1;3;31mxterm.js\x1B[0m $ ');
var input = "";
term.onData(function(data) {
    if (data != '\n') {
       input += data;
       term.write(data);
    } else {
        ipcRenderer.send('data', input);
        input = "";
    }
});