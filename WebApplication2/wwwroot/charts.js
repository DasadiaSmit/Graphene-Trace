let electrodeChart = null;
let trendChart = null;

async function renderCharts() {
    const data = await loadSensorData();
    if (!data) return;

    updateCards(data);

    const labels = data.map(x => x.index);
    const electrodeValues = data.map(x => x.electrode014);
    const avgValues = data.map(x => x.average);
    const maxValues = data.map(x => x.max);

    // ELECTRODE WAVEFORM CHART
    const ctx1 = document.getElementById("electrodeChart").getContext("2d");

    if (electrodeChart) electrodeChart.destroy();

    electrodeChart = new Chart(ctx1, {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Electrode 0.14 Signal",
                data: electrodeValues,
                borderWidth: 2,
                borderColor: "blue",
                tension: 0.3,
                pointRadius: 0
            }]
        }
    });

    // AVERAGE & PEAK TREND CHART
    const ctx2 = document.getElementById("trendChart").getContext("2d");

    if (trendChart) trendChart.destroy();

    trendChart = new Chart(ctx2, {
        type: "line",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Average",
                    data: avgValues,
                    borderColor: "green",
                    borderWidth: 2,
                    tension: 0.3,
                },
                {
                    label: "Peak",
                    data: maxValues,
                    borderColor: "red",
                    borderWidth: 2,
                    tension: 0.3,
                }
            ]
        }
    });
}

// Run once on load
renderCharts();
