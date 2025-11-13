// ===========================
// LOAD SENSOR DATA FROM BACKEND
// ===========================
async function loadSensorData() {
    try {
        const response = await fetch("/Dashboard/GetSensorData");
        const data = await response.json();

        if (!data || data.length === 0) {
            console.log("No data received from backend.");
            return;
        }

        // Update numeric cards
        document.getElementById("mainValue").innerText =
            data[data.length - 1].electrode014.toFixed(3);

        document.getElementById("avgValue").innerText =
            data[data.length - 1].averageIntensity.toFixed(3);

        document.getElementById("maxValue").innerText =
            data[data.length - 1].peakIntensity.toFixed(3);

        // Draw charts
        drawWaveform(data);
        drawTrend(data);

    } catch (error) {
        console.error("Error fetching sensor data:", error);
    }
}

// ===========================
// SENSOR WAVEFORM CHART
// ===========================
let waveformChart = null;

function drawWaveform(data) {
    const ctx = document.getElementById("waveformChart").getContext("2d");

    if (waveformChart) waveformChart.destroy();

    waveformChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: data.map(x => x.index),
            datasets: [{
                label: "Electrode 0.14",
                data: data.map(x => x.electrode014),
                borderWidth: 2,
                tension: 0.3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
}

// ===========================
// TREND CHART (AVG + MAX)
// ===========================
let trendChart = null;

function drawTrend(data) {
    const ctx = document.getElementById("trendChart").getContext("2d");

    if (trendChart) trendChart.destroy();

    trendChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: data.map(x => x.index),
            datasets: [
                {
                    label: "Average",
                    data: data.map(x => x.averageIntensity),
                    borderWidth: 2,
                    borderColor: "green",
                    tension: 0.3
                },
                {
                    label: "Peak",
                    data: data.map(x => x.peakIntensity),
                    borderWidth: 2,
                    borderColor: "red",
                    tension: 0.3
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
}

// ===========================
// AUTO REFRESH
// ===========================
loadSensorData();
setInterval(loadSensorData, 10000);
