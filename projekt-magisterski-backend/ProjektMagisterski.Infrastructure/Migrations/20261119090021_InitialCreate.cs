using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektMagisterski.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "financial_instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Currency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Exchange = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trading_strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_strategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "market_data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FinancialInstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Open = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    High = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Low = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Close = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Volume = table.Column<decimal>(type: "numeric(28,8)", precision: 28, scale: 8, nullable: false),
                    Timeframe = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_market_data_financial_instruments_FinancialInstrumentId",
                        column: x => x.FinancialInstrumentId,
                        principalTable: "financial_instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "simulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    FinancialInstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Timeframe = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    InitialCapital = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    RandomSeed = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulations_financial_instruments_FinancialInstrumentId",
                        column: x => x.FinancialInstrumentId,
                        principalTable: "financial_instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "strategy_parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TradingStrategyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DefaultValue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MinValue = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: true),
                    MaxValue = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_strategy_parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_strategy_parameters_trading_strategies_TradingStrategyId",
                        column: x => x.TradingStrategyId,
                        principalTable: "trading_strategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "simulation_agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TradingStrategyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    InitialCapital = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ConfigurationJson = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulation_agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulation_agents_simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_simulation_agents_trading_strategies_TradingStrategyId",
                        column: x => x.TradingStrategyId,
                        principalTable: "trading_strategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "simulation_configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionFeeRate = table.Column<decimal>(type: "numeric(12,8)", precision: 12, scale: 8, nullable: false),
                    SlippageBasisPoints = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    AllowShortSelling = table.Column<bool>(type: "boolean", nullable: false),
                    MaxPositionSizePercent = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulation_configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulation_configurations_simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent_performances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitialCapital = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    FinalCapital = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    TotalReturnPercentage = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    MaxDrawdown = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    NumberOfTrades = table.Column<int>(type: "integer", nullable: false),
                    WinningTrades = table.Column<int>(type: "integer", nullable: false),
                    LosingTrades = table.Column<int>(type: "integer", nullable: false),
                    WinRate = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    AverageProfit = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    AverageLoss = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    ProfitFactor = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    SharpeRatio = table.Column<decimal>(type: "numeric(12,6)", precision: 12, scale: 6, nullable: true),
                    CalculatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agent_performances_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent_snapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StepNumber = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CashBalance = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    PositionsValue = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    TotalEquity = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    RealizedProfitLoss = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    UnrealizedProfitLoss = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_snapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agent_snapshots_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Side = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    LimitPrice = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: true),
                    FilledQuantity = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Status = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExecutedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashBalance = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_portfolios_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "simulation_events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    EventType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    MetadataJson = table.Column<string>(type: "jsonb", nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulation_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulation_events_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_simulation_events_simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Side = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    ExecutionPrice = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    Slippage = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    ExecutedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trades_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trades_simulation_agents_SimulationAgentId",
                        column: x => x.SimulationAgentId,
                        principalTable: "simulation_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FinancialInstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Side = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    AverageEntryPrice = table.Column<decimal>(type: "numeric(20,8)", precision: 20, scale: 8, nullable: false),
                    OpenedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_positions_financial_instruments_FinancialInstrumentId",
                        column: x => x.FinancialInstrumentId,
                        principalTable: "financial_instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_positions_portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agent_performances_SimulationAgentId",
                table: "agent_performances",
                column: "SimulationAgentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_agent_snapshots_SimulationAgentId_StepNumber",
                table: "agent_snapshots",
                columns: new[] { "SimulationAgentId", "StepNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_financial_instruments_Symbol_Exchange",
                table: "financial_instruments",
                columns: new[] { "Symbol", "Exchange" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_market_data_FinancialInstrumentId_Timeframe_Timestamp",
                table: "market_data",
                columns: new[] { "FinancialInstrumentId", "Timeframe", "Timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_SimulationAgentId_CreatedAt",
                table: "orders",
                columns: new[] { "SimulationAgentId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_portfolios_SimulationAgentId",
                table: "portfolios",
                column: "SimulationAgentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_positions_FinancialInstrumentId",
                table: "positions",
                column: "FinancialInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_positions_PortfolioId_FinancialInstrumentId_Side",
                table: "positions",
                columns: new[] { "PortfolioId", "FinancialInstrumentId", "Side" });

            migrationBuilder.CreateIndex(
                name: "IX_simulation_agents_SimulationId",
                table: "simulation_agents",
                column: "SimulationId");

            migrationBuilder.CreateIndex(
                name: "IX_simulation_agents_TradingStrategyId",
                table: "simulation_agents",
                column: "TradingStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_simulation_configurations_SimulationId",
                table: "simulation_configurations",
                column: "SimulationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_simulation_events_SimulationAgentId",
                table: "simulation_events",
                column: "SimulationAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_simulation_events_SimulationId_Timestamp",
                table: "simulation_events",
                columns: new[] { "SimulationId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_simulations_CreatedAt",
                table: "simulations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_simulations_FinancialInstrumentId",
                table: "simulations",
                column: "FinancialInstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_simulations_Status",
                table: "simulations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_strategy_parameters_TradingStrategyId_Name",
                table: "strategy_parameters",
                columns: new[] { "TradingStrategyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trades_OrderId",
                table: "trades",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_trades_SimulationAgentId_ExecutedAt",
                table: "trades",
                columns: new[] { "SimulationAgentId", "ExecutedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_trading_strategies_Code",
                table: "trading_strategies",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agent_performances");

            migrationBuilder.DropTable(
                name: "agent_snapshots");

            migrationBuilder.DropTable(
                name: "market_data");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "simulation_configurations");

            migrationBuilder.DropTable(
                name: "simulation_events");

            migrationBuilder.DropTable(
                name: "strategy_parameters");

            migrationBuilder.DropTable(
                name: "trades");

            migrationBuilder.DropTable(
                name: "portfolios");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "simulation_agents");

            migrationBuilder.DropTable(
                name: "simulations");

            migrationBuilder.DropTable(
                name: "trading_strategies");

            migrationBuilder.DropTable(
                name: "financial_instruments");
        }
    }
}
