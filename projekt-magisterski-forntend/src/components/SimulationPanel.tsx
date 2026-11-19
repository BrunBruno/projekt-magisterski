import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

interface SimulationUpdate {
  simulationId: string;
  step: number;
  calculatedValue: number;
  message: string;
  timestamp: string;
}

const API_BASE_URL = "https://localhost:7205";

export const SimulationPanel: React.FC = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null,
  );
  const [isConnected, setIsConnected] = useState(false);
  const [isRunning, setIsRunning] = useState(false);
  const [logs, setLogs] = useState<SimulationUpdate[]>([]);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${API_BASE_URL}/hubs/simulation`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (!connection) return;

    connection
      .start()
      .then(() => {
        setIsConnected(true);

        connection.on("ReceiveSimulationUpdate", (update: SimulationUpdate) => {
          setLogs((prev) => [...prev, update]);
        });

        connection.on("SimulationCompleted", (simulationId: string) => {
          setIsRunning(false);
          console.log(`Symulacja ${simulationId} zakończona.`);
        });
      })
      .catch((err) => console.error("Błąd połączenia SignalR:", err));

    return () => {
      connection.stop();
    };
  }, [connection]);

  const handleStartSimulation = async () => {
    if (!connection || !connection.connectionId) return;

    setLogs([]);
    setIsRunning(true);

    try {
      const response = await fetch(`${API_BASE_URL}/api/simulation/start`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          connectionId: connection.connectionId,
          steps: 10,
        }),
      });

      if (!response.ok) {
        throw new Error("Nie udało się uruchomić symulacji");
      }
    } catch (error) {
      console.error(error);
      setIsRunning(false);
    }
  };

  return (
    <div
      style={{
        maxWidth: "600px",
        margin: "40px auto",
        fontFamily: "sans-serif",
      }}
    >
      <h2>Panel Symulacji</h2>
      <p>
        Status SignalR:{" "}
        <strong style={{ color: isConnected ? "green" : "red" }}>
          {isConnected ? "Połączono" : "Rozłączono"}
        </strong>
      </p>

      <button
        onClick={handleStartSimulation}
        disabled={!isConnected || isRunning}
        style={{
          padding: "10px 20px",
          fontSize: "16px",
          cursor: !isConnected || isRunning ? "not-allowed" : "pointer",
        }}
      >
        {isRunning ? "Symulacja w toku..." : "Uruchom symulację (10s)"}
      </button>

      <div
        style={{
          marginTop: "20px",
          padding: "15px",
          border: "1px solid #ccc",
          borderRadius: "6px",
          minHeight: "220px",
          backgroundColor: "#f9f9f9",
        }}
      >
        <h4>Wyniki z Workera:</h4>
        {logs.length === 0 && (
          <p style={{ color: "#666" }}>Brak danych. Kliknij start.</p>
        )}
        <ul style={{ listStyle: "none", padding: 0 }}>
          {logs.map((log) => (
            <li
              key={log.step}
              style={{
                padding: "6px 0",
                borderBottom: "1px solid #eee",
                fontFamily: "monospace",
              }}
            >
              [{new Date(log.timestamp).toLocaleTimeString()}] {log.message}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};
