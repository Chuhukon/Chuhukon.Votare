<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chuhukon.Votare.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Chuhukon - Votare</title>

	<link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/bootstrap-responsive.min.css" rel="stylesheet">
	<style>
		body {
			margin-top: 25px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
		<asp:HiddenField runat="server" ID="userId" ClientIDMode="Static"/>
	</form>

    <div class="container-fluid">
		<div class="row-fluid">
			<div class="span12">
				<div class="hero-unit">
					<h1>Votare!</h1>
				</div>
				<div class="row-fluid">
					<div class="span12">
						<p>
							<button id="btnlike" class="btn btn-large btn-success" value="like" style="display: block; width: 100%;">Like</button>
						</p>
						<p>
							<button id="btndislike" class="btn btn-large btn-danger" value="dislike" style="display: block; width: 100%;">Dislike</button>
						</p>
					</div>
				</div>
			</div>
		</div>
	</div>

	<script src="/js/jquery-1.6.4.min.js" ></script>
    <script src="/js/jquery.signalR-1.1.2.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="/signalr/hubs"></script>
	<script type="text/javascript">
		$(function () {
			var votare = $.connection.votareHub; // Declare a proxy to reference the hub. 

			votare.client.changeColor = function (color) {
				$('.hero-unit').css("background-color", color)
			};

			// Start the connection.
			$.connection.hub.start().done(function () {
				votare.server.attend($('#userId').val());

				$('#btndislike').click(function () {
					// Call the Send method on the hub. 
					votare.server.vote($('#userId').val(), false);
				});

				$('#btnlike').click(function () {
					// Call the Send method on the hub. 
					votare.server.vote($('#userId').val(), true);
				});
			});
		});
    </script>
</body>
</html>
