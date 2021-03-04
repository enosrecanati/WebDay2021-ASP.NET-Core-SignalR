import { Component, OnInit, OnDestroy } from '@angular/core';

import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

@Component({
  selector: 'app-sensor-dashboard-component',
  templateUrl: './sensor-dashboard.component.html',
  styleUrls: ['./sensor-dashboard.component.css']
})
export class SensorDashboardComponent implements OnInit, OnDestroy {
  private hubConnection: HubConnection;

  private sensors: Map<string, Array<number>> = new Map();

  async ngOnInit() {
    await this.connect();
  }

  async ngOnDestroy() {
    await this.hubConnection.invoke('DisconnectDashboard');

    this.disconnect();
  }

  public async connect() {
    this.hubConnection = new HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl('/sensorhub')
      .build();

    this.hubConnection.on('dashboardConnected', (connectedSensors) => {
      this.sensors.clear();

      connectedSensors.forEach(connectedSensor => {
        this.sensors.set(connectedSensor, []);
      });
    });

    this.hubConnection.on('sensorConnected', (sensorId) => {
      console.log('sensorConnected');

      this.sensors.set(sensorId, []);
    });

    this.hubConnection.on('sensorDisconnected', (sensorId) => {
      console.log('sensorDisconnected');

      this.sensors.delete(sensorId);
    });

    this.hubConnection.on('sensorMeasureReceived', (sensorId, value) => {
      this.sensors.set(sensorId, [...this.sensors.get(sensorId), value]);
    });

    this.hubConnection.onreconnected(async connectionId => {
      await this.hubConnection.invoke('ConnectDashboard');
    });

    await this.hubConnection.start();

    await this.hubConnection.invoke('ConnectDashboard');
  }

  public async disconnect() {
    await this.hubConnection.stop();
  }
}
