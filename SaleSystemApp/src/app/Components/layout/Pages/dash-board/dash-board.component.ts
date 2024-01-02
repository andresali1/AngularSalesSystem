import { Component, OnInit } from '@angular/core';
import { NgControl } from '@angular/forms';

import { Chart, registerables } from 'chart.js';
import { DashBoardService } from 'src/app/Services/dash-board.service';
Chart.register(...registerables);

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit {
  incomeTotal: string = '0';
  salesTotal: string = '0';
  productsTotal: string = '0';

  constructor(
    private _dashboardService: DashBoardService
  ) { }

  /**
   * Method to show the bar Chart
   * @param graphicLabel 
   * @param graphicData 
   */
  showGraphics(graphicLabel: any[], graphicData: any[]) {
    const barChart = new Chart('barChart', {
      type: 'bar',
      data: {
        labels: graphicLabel,
        datasets: [{
          label: "# de Ventas",
          data: graphicData,
          backgroundColor: [
            'rgba(54, 162, 235, 0.2)'
          ],
          borderColor: [
            'rgba(54, 162, 235, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        maintainAspectRatio: false,
        responsive: true,
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }

  ngOnInit(): void {
    this._dashboardService.summary().subscribe({
      next: (data) => {
        if (data.status) {
          this.incomeTotal = data.value.incomeTotal;
          this.salesTotal = data.value.salesTotal;
          this.productsTotal = data.value.productTotal;

          const dataArray: any[] = data.value.lastWeekSales

          const dateLabelTemp = dataArray.map((value) => value.date);
          const dataTemp = dataArray.map((value) => value.total);

          this.showGraphics(dateLabelTemp, dataTemp);
        }
      },
      error: (error) => { }
    });
  }
}
