<script src="https://cdn.jsdelivr.net/npm/vue@3.3.3/dist/vue.global.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/vue@2.6.14/dist/vue.min.js"></script>
    <script src="~/signalr.js/jquery.signalr.min.js"></script>
    <script src="~/signalr.js/jquery.signalr.js"></script>
    <script src="~/jquery/jquery.js"></script>
    <script src="~/jquery/jquery.min.js"></script>

<script>
    var connection = new signalR.HubConnectionBuilder().withUrl("/productHub").build();
    connection.on("receiveProductPath", function (value) {
        window.location.href = "/Product/DetailProduct/" + value;
        });
    connection.start().catch(function (err) {
        console.error(err.toString());
        });
</script>

