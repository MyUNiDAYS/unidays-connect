<template>
  <div class="newsletter">
    <div class="container">
      <div class="row">
        <div class="col-lg-3 col-md-3">
          <div class="power_newsletter_title">
            <h4>Subscribe to newsletter</h4>
          </div>
        </div>
        <div class="col-lg-5 col-md-6">
          <div class="power_newsletter">
            <form method="post" action="#" class="form-checkin">
              <input
                type="text"
                name="email"
                class="form-control"
                placeholder="Your E-mail"
              />
              <button type="submit" name="submit" class="newsletter_button">
                <i class="ion-ios-send"></i>
              </button>
            </form>
          </div>
        </div>
        <div class="offset-lg-1 col-lg-3 col-md-3">
          <div class="quickcontact">
            <img src="@/assets/images/phone_white.png" alt="phone" />
            <div><span>Contact us to donate</span><br />(+123)987.654.32</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
