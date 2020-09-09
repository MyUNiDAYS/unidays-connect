if (!(Test-Path "icons")) {
  Write-Error "Could not find 'icons' directory";
  return;
}
$icons = @(
  @{type="regular";width=185;height=40;suffix=""},
  @{type="regular";width=370;height=80;suffix="-large"},
  @{type="inverted";width=185;height=40;suffix=""},
  @{type="inverted";width=370;height=80;suffix="-large"}
)
foreach ($icon in $icons) {
  docker run --rm -v $PWD/icons:/icons jrbeverly/rsvg:baseimage rsvg-convert "/icons/svg/login-$($icon.type).svg" -w $icon.width -h $icon.height -f png -o "/icons/login-$($icon.type)$($icon.suffix).png";
  docker run --rm -v $PWD/icons:/icons kolyadin/pngquant -f --ext .png --quality 80-90 "/icons/login-$($icon.type)$($icon.suffix).png";
}
