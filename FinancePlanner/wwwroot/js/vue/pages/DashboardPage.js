export default {
    props: {
        CurrentIncome: Number,
        CurrentOutcome: Number,
        NetIncome: Number,
        YearlyIncome: Array,
        YearlyOutcome: Array,
        ExpensePercentagesByType: Object,
    },
    data() {
        return {
            message: "Hello from Vue!"
        }
    },
    mounted() {
        console.log("Vue test page mounted")
    },
    template: /*html*/
    `
    <div class="container-fluid" style="margin-top: 20px;"></div>
        <div class="text-center">
            <h1 class="display-4" style="margin-bottom: 20px;">Welcome to your financial future</h1>
        </div>
        <div class="row">
            <div class="col">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Income</h5>
                        <p>{{ CurrentIncome }}</p>
                    </div>
                </div>
            </div>
            
            <div class="col">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Expenses</h5>
                        <p>{{ CurrentOutcome }}</p>
                    </div>
                </div>
            </div>
            
            <div class="col">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Remaining</h5>
                        <p>{{ NetIncome }}</p>
                    </div>
                </div>
            </div>
            
            <div class="col">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Goal</h5>
                        <p id="goalDisplay"></p>
                        <select id="goalSelect" class="form-select">
                            <option value="">Select a goal</option>
                            <option value="budget">Budget</option>
                            <option value="invest">Invest</option>
                            <option value="save">Save</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <hr/>
        <div class="row">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="expenses-tab" data-bs-toggle="tab" data-bs-target="#expenses-tab-pane"
                            type="button" role="tab" aria-controls="expenses-tab-pane" aria-selected="false">Expenses Breakdown
                    </button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="projection-tab" data-bs-toggle="tab"
                            data-bs-target="#projection-tab-pane" type="button" role="tab" aria-controls="projection-tab-pane"
                            aria-selected="true">Projection
                    </button>
                </li>
            </ul>
            <div class="tab-content" id="myTabContent" style="height: 470px;">
                <div class="tab-pane fade show active" id="expenses-tab-pane" role="tabpanel" aria-labelledby="expenses-tab"
                     tabindex="0">
                    <div class="row">
                        <div class="col-md-6" style="height: 400px;">
                            <canvas id="expensesPercentageChart"></canvas>
                        </div>
            
                        <div class="col-md-6">
                            <div class=card>
                                <div class="card-body">
                                    <h5 class="card-title">Expenses Settings</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="projection-tab-pane" role="tabpanel" aria-labelledby="projection-tab"
                     tabindex="0">
                    <div class="row">
                        <div class="col-md-6" style="height: 400px;">
                            <canvas id="projectionChart"></canvas>
                        </div>
            
                        <div class="col-md-6">
                            <div class=card>
                                <div class="card-body">
                                    <h5 class="card-title">Projection Settings</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    `
}