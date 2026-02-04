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
        <p>{{ message }}</p>
    </div>
    `
}