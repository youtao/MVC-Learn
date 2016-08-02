const electron = require('electron');
const {app} = electron;
const {BrowserWindow} = electron;
const path = require('path');

const htmlPath = path.join('file://', __dirname, 'html/modal.html');

let mainWindow;
        
app.on('ready', function () {
    // Create the browser window.
    mainWindow = new BrowserWindow({ width:800, height: 600 });
    
    // and load the index.html of the app.
    mainWindow.loadURL(htmlPath);
    
    // Open the DevTools.
    mainWindow.webContents.openDevTools();
    
    // Emitted when the window is closed.
    mainWindow.on('closed', function () {
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow = null;
    });
});