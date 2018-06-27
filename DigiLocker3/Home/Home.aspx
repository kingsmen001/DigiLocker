﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DigiLocker3.Home.Home" %>

<!DOCTYPE html>

<html>
<head>
	<title>DigiLocker-A digital locker to safely keep your documents.</title>
	<!--/tags -->
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="keywords" content="Surf Responsive web template, Bootstrap Web Templates, Flat Web Templates, Android Compatible web template, 
Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
	<script type="application/x-javascript">
		addEventListener("load", function () {
			setTimeout(hideURLbar, 0);
		}, false);

		function hideURLbar() {
			window.scrollTo(0, 1);
		}
	</script>
   
	<!--//tags -->
	<link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
	<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
	<link href="css/font-awesome.css" rel="stylesheet">
	<!-- //for bootstrap working -->
	<link href="//fonts.googleapis.com/css?family=Montserrat:200,200i,300,400,400i,500,500i,600,600i,700,700i,800" rel="stylesheet">
	<link href='//fonts.googleapis.com/css?family=Lato:400,100,100italic,300,300italic,400italic,700,900,900italic,700italic'
	    rel='stylesheet' type='text/css'>
</head>

<body>
	<!-- header_top -->
	<div class="header" id="home">
		<div class="banner_header_top_agile_w3ls">
			<div class="header-top-wthree-agileits">
				<nav class="navbar navbar-default">
					<div class="navbar-header navbar-left">
						<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
									<span class="sr-only">Toggle navigation</span>
									<span class="icon-bar"></span>
									<span class="icon-bar"></span>
									<span class="icon-bar"></span>
								</button>
						<h1><a class="navbar-brand" href="index.html"> DigiLocker</a></h1>
					</div>
									<div class="header_left">
						<ul>
							<li><a class="shop" style ="color:red">Sign In</a></li>
							<li><a class="sign" href="#" data-toggle="modal" data-target="#myModal2"><i class="fa fa-user" aria-hidden="true"></i></a></li>

						</ul>
					</div>
					<!-- Collect the nav links, forms, and other content for toggling -->
					<div class="collapse navbar-collapse navbar-right" id="bs-example-navbar-collapse-1">

						<div class="top_nav_left">
							<nav class="cl-effect-15" id="cl-effect-15">
								<ul>
									<li class="active"><a href="index.html" data-hover="Home">Home</a></li>
									<li><a href="about.html" data-hover="About">About</a></li>
									<li><a href="blog.html" data-hover="Blog">Blog</a></li>
									<li class="dropdown">
										<a href="services.html" data-hover="Drop Down" class="dropdown-toggle" data-toggle="dropdown">Drop Down <b class="fa fa-angle-down"></b></a>
										<ul class="dropdown-menu">
											<li><a href="404.html">Services</a></li>
											<li class="divider"></li>
											<li><a class="s-menu" href="#">Pages <b class="fa fa-angle-right"></b></a>
												<ul class="dropdown-menu sub-menu">
													<li><a href="404.html">Error</a></li>
													<li class="divider"></li>
													<li><a href="gallery.html">Gallery</a></li>
												</ul>
											</li>

										</ul>
									</li>

									<li><a href="Contact.html" data-hover="Contact">Contact</a></li>
								</ul>
							</nav>
						</div>
					</div>
					
					<!-- search -->
					<div class="search">
						<div class="cd-main-header">
							<ul class="cd-header-buttons">
								<li><a class="cd-search-trigger" href="#cd-search"> <span></span></a></li>
							</ul>
						</div>
						<div id="cd-search" class="cd-search">
							<form action="#" method="post">
								<input name="Search" type="search" placeholder="Click enter after typing...">
							</form>
						</div>
					</div>
					<!-- //search -->
					<div class="clearfix"></div>
				</nav>
			</div>
		</div>
		<!-- banner -->
		<div id="myCarousel" class="carousel slide" data-ride="carousel">
			<ol class="carousel-indicators">
				<li data-target="#myCarousel" data-slide-to="0" class="active"></li>
				<li data-target="#myCarousel" data-slide-to="1" class=""></li>
				<li data-target="#myCarousel" data-slide-to="2" class=""></li>
				<li data-target="#myCarousel" data-slide-to="3" class=""></li>
			</ol>
			<div class="carousel-inner" role="listbox">
				<div class="item active">
					<div class="container">
						<div class="carousel-caption">
							<h3>Surfing is a <span>real life</span></h3>
							<h4>Sea Sand & Surf</h4>
							<p>Add Some Description</p>

						</div>
					</div>
				</div>
				<div class="item item2">
					<div class="container">
						<div class="carousel-caption">
							<h3>Shine <span>with waves </span></h3>
							<h4>Do it like a Surfer</h4>
							<p>Add Some Description</p>
						</div>
					</div>
				</div>
				<div class="item item3">
					<div class="container">
						<div class="carousel-caption">
							<h3>Surfing is a <span>real life</span></h3>
							<h4>Do it like a Surfer</h4>
							<p>Add Some Description</p>

						</div>
					</div>
				</div>
				<div class="item item4">
					<div class="container">
						<div class="carousel-caption">
							<h3>Rise <span>with waves </span></h3>
							<h4>Sea Sand & Surf</h4>
							<p>Add Some Description</p>
						</div>
					</div>
				</div>
			</div>
			<a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
			<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
			<span class="sr-only">Previous</span>
		</a>
			<a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
			<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
			<span class="sr-only">Next</span>
		</a>
			<!-- The Modal -->
		</div>
		<!--//banner -->
		<!--//banner -->

		<!-- Modal1 -->
		<div class="modal fade" id="myModal2" tabindex="-1" role="dialog">
			<div class="modal-dialog">
				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>

						<div class="signin-form profile">
							<h3 class="sign">Sign In</h3>
							<div class="login-form">
								<form id="form1" runat="server">
									<%--<input type="email" name="email" placeholder="E-mail" required="">
									<input type="password" name="password" placeholder="Password" required="">
									<div class="tp">
										<input runat="server" type="submit" value="Sign In" onserverclick="SigninClicked">
                                        <asp:Button ID="Button1" runat="server" Text="Button"  />
									</div>--%>
                                    <asp:TextBox ID="PNo_TextBox" runat="server" placeholder="Personal Number"></asp:TextBox>
									<%--<input type="password" name="password" placeholder="Password" required="">--%>
                                    <asp:TextBox ID="PAssword_TextBox" runat="server" placeholder="Password"></asp:TextBox>
                                    <asp:Button runat="server" id="LoginButton" text="Login" onclick="LoginButton_Click"/>
								</form>
							</div>
							<div class="login-social-grids">
								<ul>
									<li><a href="#"><i class="fa fa-facebook"></i></a></li>
									<li><a href="#"><i class="fa fa-twitter"></i></a></li>
									<li><a href="#"><i class="fa fa-rss"></i></a></li>
								</ul>
							</div>
							<p><a href="#" data-toggle="modal" data-target="#myModal3"> Don't have an account?</a></p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- //Modal1 -->
		<!-- Modal2 -->
		<div class="modal fade" id="myModal3" tabindex="-1" role="dialog">
			<div class="modal-dialog">
				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>

						<div class="signin-form profile">
							<h3 class="sign">Sign Up</h3>
							<div class="login-form">
								<form action="#" method="post">
									<input type="text" name="name" placeholder="Username" required="">
									<input type="email" name="email" placeholder="Email" required="">
									<input type="password" name="password" placeholder="Password" required="">
									<input type="password" name="password" placeholder="Confirm Password" required="">
									<input type="submit" value="Sign Up">
								</form>
							</div>
							<p><a href="#"> By clicking Sign up, I agree to your terms</a></p>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- //Modal2 -->
	</div>
	<!--// header_top -->
	<!--about-->
	<div class="ab_content">
		<div class="container">
			<h3 class="tittle-w3ls">About Us</h3>
			<div class="about-top">
				<h3 class="subheading">With love for the sport of Surf Inn</h3>
				<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer quis tristique est, et egestas odio. Mauris ac tristique
					arcu, sed interdum risus.Integer quis tristique est, et egestas odio. Mauris ac tristique arcu, sed interdum risus.
				</p>
			</div>
			<div class="about-main">
				<div class="col-md-6 about-left">
				</div>

				<div class="col-md-6 about-right">
					<h3>Towards wind and waves</h3>
					<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer quis tristique est, et egestas odio, sed interdum risus.</p>

				</div>
				<div class="clearfix"> </div>
			</div>
			<div class="about-main sec">

				<div class="col-md-6 about-right two">
					<h3>Towards wind and waves</h3>
					<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer quis tristique est, et egestas odio, sed interdum risus.</p>

				</div>
				<div class="col-md-6 about-left two">
				</div>

				<div class="clearfix"> </div>
			</div>
		</div>
	</div>
	<!--//about-->
	<!--/works-->
	<div class="works">
		<div class="container">
			<h3 class="tittle-w3ls cen">What we Provide</h3>
			<div class="inner_sec_info_w3ls_agile">
				<div class="ser-first">
					<div class="col-md-3 ser-first-grid text-center">
						<span class="fa fa-shield" aria-hidden="true"></span>
						<h3>Stand Up Paddle</h3>
						<p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
					</div>
					<div class="col-md-3 ser-first-grid text-center">
						<span class="fa fa-pencil" aria-hidden="true"></span>
						<h3>Day Tours</h3>
						<p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
					</div>
					<div class="col-md-3 ser-first-grid text-center">
						<span class="fa fa-star" aria-hidden="true"></span>
						<h3>After Schools</h3>
						<p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
					</div>
					<div class="col-md-3 ser-first-grid text-center">
						<span class="fa fa-thumbs-up" aria-hidden="true"></span>
						<h3>Gift Vouchers</h3>
						<p>Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.</p>
					</div>
					<div class="clearfix"></div>
				</div>
			</div>

		</div>
	</div>
	<!--/works-->
	<!-- /portfolio-->
	<!-- <div class="portfolio-project">
		<div class="container">
	<h3 class="tittle-w3ls cen">Gallery</h3>
			<div class="inner_sec_info_w3ls_agile">
					<div class="col-md-6 portfolio-grids_left">
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g1.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g1.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
								   <h4>Surf Inn</h4>
								</div>
							</a>
						</div>
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g2.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g2.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
								  <h4>Surf Inn</h4>
									
								</div>
							</a>
						</div>
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g3.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g3.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
								  <h4>Surf Inn</h4>
									
								</div>
							</a>
						</div>
					</div>
					<div class="col-md-6 portfolio-grids sec_img" data-aos="zoom-in">
						<a href="images/g10.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g10.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
									  <h4>Surf Inn</h4>
									
								</div>
							</a>
					</div>
						<div class="col-md-6 portfolio-grids sec_img" data-aos="zoom-in">
						<a href="images/g11.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g11.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
									  <h4>Surf Inn</h4>
									
								</div>
							</a>
					</div>
					<div class="col-md-6 portfolio-grids_left">
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g5.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g5.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
									  <h4>Surf Inn</h4>
									
								</div>
							</a>
						</div>
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g4.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g4.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
								  <h4>Surf Inn</h4>
								</div>
							</a>
						</div>
						<div class="col-md-6 portfolio-grids" data-aos="zoom-in">
							<a href="images/g6.jpg" class="b-link-stripe b-animate-go lightninBox" data-lb-group="1">
								<img src="images/g6.jpg" class="img-responsive" alt=" " />
								<div class="b-wrapper">
								  <h4>Surf Inn</h4>
									
								</div>
							</a>
						</div>

					</div>
					<div class="clearfix"> </div>
				</div>
		</div>
	</div>-->

	<!--/ pricing-->
	<!--/<div class="pricing">
		<div class="container">
			<h3 class="tittle-w3ls cen">Pricing Table</h3>
			<div class="inner_sec_info_w3ls_agile">
				<div class="col-md-4 pricing_inner_main">
					<div class="pricing-top">
						<h3>Starter</h3>
						<p>$<span>35</span></p>
					</div>
					<div class="pricing-bottom">
						<div class="pricing-bottom-bottom">
							<p>Colorful and Fun</p>
							<p>Rarely Lets You Down</p>
							<p>Environmentally Friendly</p>
							<p>Sometimes Disappears</p>

						</div>

						<div class="sign text-center">
							<a class="popup-with-zoom-anim" href="#" data-toggle="modal" data-target="#myModal4">BOOK NOW</a>
						</div>
					</div>
				</div>
				<div class="col-md-4 pricing_inner_main active">
					<div class="pricing-top orange">
						<h3>Advanced</h3>
						<p>$<span>175</span></p>
					</div>
					<div class="pricing-bottom">
						<div class="pricing-bottom-bottom">
							<p>Feels Great</p>
							<p>Mildly Confusing</p>
							<p>Mildly Confusing</p>
							<p>Colorless on Wednesday</p>

						</div>

						<div class="sign text-center">
							<a class="popup-with-zoom-anim orange active" href="#" data-toggle="modal" data-target="#myModal4">BOOK NOW</a>
						</div>
					</div>
				</div>
				<div class="col-md-4 pricing_inner_main">
					<div class="pricing-top purple">
						<h3>Developer</h3>
						<p>$<span>190</span></p>
					</div>
					<div class="pricing-bottom">
						<div class="pricing-bottom-bottom">
							<p>Really the Best</p>
							<p>Sometimes the Best</p>
							<p>Not Quite the Best</p>
							<p>Truly a must have</p>

						</div>

						<div class="sign text-center">
							<a class="popup-with-zoom-anim purple" href="#" data-toggle="modal" data-target="#myModal4">BOOK NOW</a>
						</div>
					</div>
				</div>
				<div class="clearfix"></div>
			</div>
		</div>
	</div>-->
	<!-- Popup-Box -->
<!-- Modal2 -->
		<%--<div class="modal fade" id="myModal4" tabindex="-1" role="dialog">
			<div class="modal-dialog">
				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>

			<div class="pop_up">
				<div class="payment-online-form-left">
					<form action="#" method="post">
						<h4>Personal Details</h4>
						<ul>
							<li><input type="text" name="First Name" placeholder="First Name" required=""></li>
							<li><input type="text" name="Last Name" placeholder="Last Name" required=""></li>
						</ul>
						<ul>
							<li><input type="email" class="email" name="Email" placeholder="Email" required=""></li>
							<li><input type="text" name="Number" placeholder="Mobile Number" required=""></li>
						</ul>
						<textarea name="Message" placeholder="Address..." required=""></textarea>
						<div class="clearfix"></div>

						<ul class="payment-sendbtns">
							<li><input type="reset" value="Reset"></li>
							<li><input type="submit" name="Send" value="Send"></li>
						</ul>
						<div class="clearfix"></div>
					</form>
				</div>
			</div>
		</div>
	</div>
</div>
</div>
	<!-- //Popup-Box -->

	<!--// pricing-->

	<!-- Testimonials -->
	<div class="testimonials">
		<div class="container">
			<h3 class="tittle-w3ls">What people says</h3>
			<div class="inner_sec_info_w3ls_agile">
				<div class="col-md-6 testimonials-main">
						<div class="carousel slide two" data-ride="carousel" id="quote-carousel">
							<!-- Bottom Carousel Indicators -->
							<ol class="carousel-indicators two">
								<li data-target="#quote-carousel" data-slide-to="0" class="active"></li>
								<li data-target="#quote-carousel" data-slide-to="1"></li>
								<li data-target="#quote-carousel" data-slide-to="2"></li>
							</ol>

						<div class="carousel-inner">
								<div class="item active">

									<div class="inner-testimonials">
										<img src="images/1.jpg" alt=" " class="img-responsive" />
										<div class="teastmonial-info">
											<h5>Andy Wovel</h5>
											<span>Lorem Ipsum</span>
											<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis hendrerit lobortis elementum, Quis nostrum exercitationem
												ullam corporis suscipit laboriosam. </p>
										</div>
									</div>
								</div>
								<div class="item">

									<div class="inner-testimonials">
										<img src="images/2.jpg" alt=" " class="img-responsive" />
										<div class="teastmonial-info">
											<h5>Bernard Russo</h5>
											<span>Lorem Ipsum</span>
											<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis hendrerit lobortis elementum, Quis nostrum exercitationem
												ullam corporis suscipit laboriosam. </p>
										</div>
									</div>
								</div>
								<div class="item">

									<div class="inner-testimonials">
										<img src="images/3.jpg" alt=" " class="img-responsive" />
										<div class="teastmonial-info">
											<h5>Alex Merphy</h5>
											<span>Lorem Ipsum</span>
											<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis hendrerit lobortis elementum, Quis nostrum exercitationem
												ullam corporis suscipit laboriosam. </p>
										</div>
									</div>
								</div>
								<!-- Carousel Buttons Next/Prev -->
							<a data-slide="prev" href="#quote-carousel" class="left carousel-control"><i class="fa fa-chevron-left"></i></a>
							<a data-slide="next" href="#quote-carousel" class="right carousel-control"><i class="fa fa-chevron-right"></i></a>

						</div>
				</div>

				</div>
			</div>
		</div>
	</div>--%>
	<!-- //Testimonials -->
	<!-- footer -->
	<footer>
		<div class="footer-top-w3-agileits">
			<div class="container">
				<div class="footer-grid-wthree-agiles">
					<div class="col-md-5 footer-grid-wthree-agile">
						<h3>About us</h3>
						<p>Lorem ipsum dolor sit consectetur elit. Nam eget egestas erat. In hachabi tasse platea dictumst. hachabi tasse platea
							dictumst
						</p>
						<p>Lorem ipsum dolor sit consectetur elit. Nam eget egestas erat. In hachabi tasse platea dictumst.</p>
						<ul class="footer_list_icons">
							<li><a href="#"><i class="fa fa-facebook"></i></a></li>
							<li><a href="#"><i class="fa fa-twitter"></i></a></li>
							<li><a href="#"><i class="fa fa-google-plus"></i></a></li>
							<li><a href="#"><i class="fa fa-linkedin"></i></a></li>
							<li><a href="#"><i class="fa fa-vimeo" aria-hidden="true"></i></a></li>
							<li><a href="#"><i class="fa fa-youtube" aria-hidden="true"></i></a></li>
						</ul>
					</div>
					<div class="col-md-3 footer-grid-wthree-agile">
						<h3>Links</h3>
						<ul class="footer-list">
							<li> <a href="about.html">About</a> </li>
							<li> <a href="gallery.html">Gallery</a> </li>
							<li> <a href="shop.html">Shop</a> </li>
							<li> <a href="blog.html">Blog</a> </li>
							<li> <a href="404.html">Events</a> </li>
							<li> <a href="contact.html">Contact</a> </li>
						</ul>
					</div>
					<div class="col-md-4 footer-grid-wthree-agile">
						<h3>Newsletter</h3>
						<form action="#" method="post">
							<input type="text" name="text" placeholder="Name" required="">
							<input type="email" name="Email" placeholder="Email" required="">
							<input type="submit" value="subscribe now">
						</form>
					</div>
					<div class="clearfix"> </div>
				</div>
				<div class="copy_right">
					<p>©  2018 Surf Inn. All rights reserved</p>
				</div>
			</div>
		</div>

	</footer>
	<!-- //footer -->
	<a href="#home" class="scroll" id="toTop" style="display: block;"> <span id="toTopHover" style="opacity: 1;"> </span></a>
	<!-- js -->
	<script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>

	<!--search-bar-->
	<script src="js/search.js"></script>
	<!--//search-bar-->
			
<!-- start-smoth-scrolling -->
	<script type="text/javascript" src="js/move-top.js"></script>
	<script type="text/javascript" src="js/jquery.easing.min.js"></script>
	<script type="text/javascript">
		jQuery(document).ready(function ($) {
			$(".scroll").click(function (event) {
				event.preventDefault();

				$('html,body').animate({
					scrollTop: $(this.hash).offset().top
				}, 1000);
			});
		});
	</script>
	<!-- //end-smooth-scrolling -->
	<!-- Pricing-Popup-Box-JavaScript -->
	<script src="js/jquery.magnific-popup.js" type="text/javascript"></script>
	<!-- //Pricing-Popup-Box-JavaScript -->
	<script>
		$('ul.dropdown-menu li').hover(function () {
			$(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
		}, function () {
			$(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
		});
	</script>
 <!-- js for portfolio lightbox -->
	<script src="js/jquery.chocolat.js "></script>
	<link rel="stylesheet " href="css/chocolat.css " type="text/css " media="screen ">
	<!--light-box-files -->
	<script type="text/javascript ">
		$(function () {
			$('.portfolio-grids a').Chocolat();
		});
	</script>
	<!-- /js for portfolio lightbox -->

	<!-- smooth-scrolling-of-move-up -->
	<script type="text/javascript">
		$(document).ready(function () {
			/*
			var defaults = {
				containerID: 'toTop', // fading element id
				containerHoverID: 'toTopHover', // fading element hover id
				scrollSpeed: 1200,
				easingType: 'linear' 
			};
			*/

			$().UItoTop({
				easingType: 'easeOutQuart'
			});

		});
	</script>
	<!-- //smooth-scrolling-of-move-up -->

	<script type="text/javascript" src="js/bootstrap.js"></script>
</body>

</html>